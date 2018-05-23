using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Study_App_API.MongoDB_Commands;
using MongoDB_Standard.models;

namespace Study_App_API.Controllers {

    internal class GoalComparer : IEqualityComparer<Goal> {
        public bool Equals(Goal x, Goal y) {
            return x.GUID == y.GUID;
        }

        public int GetHashCode(Goal obj) {
            return obj.GetHashCode();
        }
    }

    public class CalendarController : Controller {

        private ServerIO serverInterface = new ServerIO();

        // TODO: test if this works, not 100% sure
        [HttpGet]
        public JsonResult GetMonth(string username, int monthOfYear) {
            Month month = new Month {
                Days = new List<Day>(),
                MonthOfYear = monthOfYear
            };
            UserAccount user = serverInterface.GetUser(username);
            Dictionary<Goal, DateTime> recGoals = GetApplicableMonthRecurringGoals(monthOfYear, user);

            foreach (Goal goal in user.ListOfGoals.Except(recGoals.Keys, new GoalComparer())) {
                DateTime goalDeadline = goal.Deadline;
                if (goalDeadline.Month == monthOfYear) {
                    Day day;
                    if ((day = month.Days.SingleOrDefault(d => d.DayOfMonth == goalDeadline.Day)) != null) {
                        day.Goals.Add(goal);
                    } else {
                        day = new Day {
                            Goals = new List<Goal> {
                                goal
                            },
                            DayOfMonth = goalDeadline.Day
                        };

                        month.Days.Add(day);
                    }
                }
            }

            foreach (KeyValuePair<Goal, DateTime> recGoal in recGoals) {
                DateTime goalDeadline = recGoal.Value;
                Goal goal = recGoal.Key;
                if (goalDeadline.Month == monthOfYear) {
                    Day day;
                    if ((day = month.Days.SingleOrDefault(d => d.DayOfMonth == goalDeadline.Day)) != null) {
                        day.Goals.Add(goal);
                    } else {
                        day = new Day {
                            Goals = new List<Goal> {
                                goal
                            },
                            DayOfMonth = goalDeadline.Day
                        };

                        month.Days.Add(day);
                    }
                }
            }

            return Json(month, JsonRequestBehavior.AllowGet);
        }

        private Dictionary<Goal, DateTime> GetApplicableMonthRecurringGoals(int curMonth, UserAccount user) {
            Dictionary<Goal, DateTime> recurringGoals = new Dictionary<Goal, DateTime>();
            foreach (Goal goal in user.ListOfGoals) {
                if (goal is RecurringGoal recGoal) {
                    DateTime date = recGoal.Deadline;
                    for (; date.Month < curMonth; date += recGoal.Frequency) { }

                    if (date.Month == curMonth) {
                        recurringGoals.Add(goal, date);
                    }
                }
            }

            return recurringGoals;
        }
    }
}
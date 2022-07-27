using KaelStore.Domain.Entities;
using RulesEngine.Enums;
using RulesEngine.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KaelStore.Service.Rules
{
    public class PromotionRule
    {
        public RulesEngine.RulesEngine RulesEngine { get; set; }
        public PromotionRule()
        {
            var rules = new List<Rule>();

            var actionRule = new Dictionary<ActionTriggerType, ActionInfo>()
            {
                { 
                    ActionTriggerType.onSuccess,
                    new ActionInfo 
                    {
                        Context = new Dictionary<string, object>()
                        {
                            { "WorkflowName", "Discount" },
                            { "ruleName", "GiveDiscount10%" },
                            { "inputFilter", new string[] { "input3" } }
                        },
                        Name = "EvaluateRule"
                    }
                }
            };

            var rule = new Rule()
            {
                RuleName = "ATK beli 10 discount 10%",
                RuleExpressionType = RuleExpressionType.LambdaExpression,
                Expression = "input1 == 1 AND input2 >= 10",
                Enabled = true,
                Actions = actionRule
            };

            rules.Add(rule);

            var workflowRules = new WorkflowRules()
            {
                WorkflowName = "Discount",
                Rules = rules
            };

            WorkflowRules[] wfrs =
            {
                workflowRules
            };

            RulesEngine = new RulesEngine.RulesEngine(wfrs, null);
        }

        public async Task<decimal> Check(List<OrderDetail> input)
        {
            var grouppedItems = input.GroupBy(od => od.Product.CategoryId)
                .Select(g => new
                {
                    Id = g.Key,
                    TotalItems = g.Sum(od => od.Quantity),
                    TotalPrice = g.Sum(od => od.Product.Price * od.Quantity)
                });

            var resList = new List<RuleResultTree>();

            decimal discount = 0;

            foreach(var i in grouppedItems)
            {
                var res = await RulesEngine.ExecuteAllRulesAsync("Discount", i.Id, i.TotalItems, i.TotalPrice);
                if (res.Any()) resList.AddRange(res);
            }

            foreach (var rt in resList)
            {
                if (rt.IsSuccess)
                {
                    discount += decimal.Parse(rt.ActionResult.Output.ToString());
                }
            }

            if (discount > 20000)
            {
                return 20000;
            }

            return discount;
        }
    }
}

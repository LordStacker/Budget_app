using infrastructure.Repositories;

namespace service;

public class BudgetService
{
    private readonly BudgetRepository _budgetRepository;
    
    public BudgetService(BudgetRepository budgetRepository)
    {
        _budgetRepository = budgetRepository;
    }

}

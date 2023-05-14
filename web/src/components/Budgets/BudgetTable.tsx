import { ChangeEvent, useEffect, useState } from "react";
import { DOWN_ARROW, UP_ARROW } from "../../utils/constants";
import {
  Budget,
  BudgetTableProps,
  SortConfig,
} from "../../types/componentTypes";
import { SortDirection } from "../../types/utilsTypes";

import "./Budgets.css";

function BudgetTable({ budgets, categories }: BudgetTableProps) {
  const [filteredBudgets, setFilteredBudgets] =
    useState<Array<Budget>>(budgets);
  const [filterValue, setFilterValue] = useState("");
  const [sortConfig, setSortConfig] = useState<SortConfig>({
    key: "month",
    direction: "asc",
  });

  useEffect(() => {
    setFilteredBudgets([...budgets]);
  }, [budgets]);

  const handleFilterChange = (e: ChangeEvent<HTMLInputElement>) => {
    const value = e.target.value;
    setFilterValue(value);

    const filteredBudgets = budgets.filter((budget) => {
      const categoryMatch = categories[budget.categoryId]
        ?.toLowerCase()
        .includes(value.toLowerCase());
      const otherMatches =
        budget.month.toLowerCase().includes(value.toLowerCase()) ||
        budget.income.toString().includes(value.toString()) ||
        budget.expenses.toString().includes(value.toString()) ||
        budget.netIncome.toString().includes(value.toString());

      return categoryMatch || otherMatches;
    });

    setFilteredBudgets(filteredBudgets);
  };

  const handleSort = (key: keyof Budget) => {
    const direction: SortDirection =
      sortConfig.key === key && sortConfig.direction === "asc" ? "desc" : "asc";

    const sortedBudgets = [...filteredBudgets].sort((a, b) => {
      if (a[key] < b[key]) {
        return direction === "asc" ? -1 : 1;
      }
      if (a[key] > b[key]) {
        return direction === "asc" ? 1 : -1;
      }
      return 0;
    });

    setFilteredBudgets(sortedBudgets);
    setSortConfig({ key, direction });
  };

  const renderSortIndicator = (key: keyof Budget) => {
    if (sortConfig.key === key) {
      return sortConfig.direction === "asc" ? UP_ARROW : DOWN_ARROW;
    }
    return null;
  };

  return (
    <div>
      <input
        type="text"
        placeholder="Filter by anything"
        value={filterValue}
        onChange={handleFilterChange}
      />
      <div className="budget-table-container">
        <table className="budget-table">
          <thead>
            <tr>
              <th onClick={() => handleSort("month")}>
                Month {renderSortIndicator("month")}
              </th>
              <th onClick={() => handleSort("categoryId")}>
                Category {renderSortIndicator("categoryId")}
              </th>
              <th onClick={() => handleSort("income")}>
                Income {renderSortIndicator("income")}
              </th>
              <th onClick={() => handleSort("expenses")}>
                Expenses {renderSortIndicator("expenses")}
              </th>
              <th onClick={() => handleSort("netIncome")}>
                Net Income {renderSortIndicator("netIncome")}
              </th>
              <th>Shared</th>
            </tr>
          </thead>
          <tbody>
            {filteredBudgets.map((budget, index) => (
              <tr key={index}>
                <td>{budget.month}</td>
                <td>{categories[budget.categoryId]}</td>
                <td>{budget.income}</td>
                <td>{budget.expenses}</td>
                <td
                  className={
                    budget.netIncome < 0 ? "negative-income" : "positive-income"
                  }
                >
                  {budget.netIncome}
                </td>
                <td>{budget.shared ? "yes" : "no"}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}
export default BudgetTable;

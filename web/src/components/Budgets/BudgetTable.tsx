import React, { ChangeEvent, useEffect, useState } from "react";
import "./BudgetTable.css";
import { Budget } from "./Budgets";
import { DOWN_ARROW, UP_ARROW } from "../../utils/constants";

type SortDirection = "asc" | "desc";

type SortConfig = {
  key: keyof Budget;
  direction: SortDirection;
};

function BudgetTable({ budgets }: { budgets: Array<Budget> }) {
  const [filteredBudgets, setFilteredBudgets] = useState<Budget[]>([
    ...budgets,
  ]);
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

    const filteredBudgets = budgets.filter(
      (budget) =>
        budget.month.toLowerCase().includes(value.toLowerCase()) ||
        budget.category.toLowerCase().includes(value.toLowerCase()) ||
        budget.income.toString().includes(value.toString()) ||
        budget.expenses.toString().includes(value.toString()) ||
        budget.netIncome.toString().includes(value.toString())
    );
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
      <table className="budget-table">
        <thead>
          <tr>
            <th onClick={() => handleSort("month")}>
              Month {renderSortIndicator("month")}
            </th>
            <th onClick={() => handleSort("category")}>
              Category {renderSortIndicator("category")}
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
          </tr>
        </thead>
        <tbody>
          {filteredBudgets.map((budget, index) => (
            <tr key={index}>
              <td>{budget.month}</td>
              <td>{budget.category}</td>
              <td>{budget.income}</td>
              <td>{budget.expenses}</td>
              <td
                className={
                  budget.netIncome < 0 ? "negative-income" : "positive-income"
                }
              >
                {budget.netIncome}
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
export default BudgetTable;

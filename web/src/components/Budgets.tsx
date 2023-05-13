import { useEffect, useState } from "react";
import { fetchBudgets } from "../services/BudgetService";
import "./BudgetTable.css";

type Budget = {
  name: string;
  amount: number;
};

const Budgets = () => {
  const [budgets, setBudgets] = useState<Array<Budget>>([]);

  useEffect(() => {
    const requestController = new AbortController();

    const getBudgets = async () => {
      try {
        let response = await fetchBudgets(requestController);
        console.log(response);
        setBudgets(response?.data);
      } catch (error) {
        console.error(error);
      }
    };

    getBudgets();

    return () => {
      requestController.abort();
    };
  }, []);

  return (
    <section>
      <h1>Budgets</h1>
      <br />
      <table className="budget-table">
        <thead>
          <tr>
            <th>Name</th>
            <th>Amount</th>
          </tr>
        </thead>
        <tbody>
          {budgets.map((budget, index) => (
            <tr key={index}>
              <td>{budget.name}</td>
              <td>{budget.amount}</td>
            </tr>
          ))}
        </tbody>
      </table>
      <br />
    </section>
  );
};

export default Budgets;

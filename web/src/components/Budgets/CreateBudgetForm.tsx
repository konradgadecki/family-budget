import { FormEvent, useState } from "react";
import useAuth from "../../hooks/useAuth";
import { createBudget } from "../../services/BudgetService";
import {
  CreateBudgetFormProps,
  CreateNewBudget,
} from "../../types/componentTypes";

import "./Budgets.css";

function CreateBudgetForm({
  onCancel,
  onCreate,
  categories,
}: CreateBudgetFormProps) {
  const { auth } = useAuth();

  const [month, setMonth] = useState("");
  const [category, setCategory] = useState(0);
  const [income, setIncome] = useState(0);
  const [expenses, setExpenses] = useState(0);
  const [shared, setShared] = useState(false);
  const [errMsg, setErrMsg] = useState("");

  const onCancelClicked = () => {
    onCancel();
  };

  const handleSubmit = async (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    const newBudget: CreateNewBudget = {
      month: month,
      categoryId: category,
      income: income,
      expenses: expenses,
      shared: shared,
    };

    try {
      await createBudget(newBudget, auth.accessToken);
      onCreate({ ...newBudget, netIncome: income - expenses });
    } catch (err) {
      setErrMsg("Error has occured");
    }
  };

  return (
    <>
      <p
        className={errMsg ? "errorMessage" : "offscreen"}
        aria-live="assertive"
      >
        {errMsg}
      </p>
      <button
        className="cancel-budget-button"
        onClick={() => onCancelClicked()}
      >
        Cancel
      </button>
      <form onSubmit={handleSubmit} className="form-container">
        <h2 className="create-budget-heading">Create new Budget</h2>
        <table className="create-budget-table">
          <tbody>
            <tr>
              <td>
                <label htmlFor="month">Month:</label>
              </td>
              <td>
                <input
                  type="text"
                  id="month"
                  autoComplete="off"
                  onChange={(e) => setMonth(e.target.value)}
                  value={month}
                  required
                />
              </td>
            </tr>
            <tr>
              <td>
                <label htmlFor="category">Category:</label>
              </td>
              <td>
                <select
                  id="user-select"
                  value={category || ""}
                  onChange={(e) => setCategory(parseInt(e.target.value))}
                >
                  <option>Select Category</option>
                  {Object.entries(categories).map(([value, label]) => {
                    return (
                      <option key={value} value={value}>
                        {label}
                      </option>
                    );
                  })}
                </select>
              </td>
            </tr>
            <tr>
              <td>
                <label htmlFor="income">Income:</label>
              </td>
              <td>
                <input
                  type="number"
                  id="income"
                  autoComplete="off"
                  onChange={(e) => setIncome(parseInt(e.target.value))}
                  value={income}
                  required
                />
              </td>
            </tr>
            <tr>
              <td>
                <label htmlFor="expenses">Expenses:</label>
              </td>
              <td>
                <input
                  type="number"
                  id="expenses"
                  autoComplete="off"
                  onChange={(e) => setExpenses(parseInt(e.target.value))}
                  value={expenses}
                  required
                />
              </td>
            </tr>
            <tr>
              <td>
                <label htmlFor="shared">Shared:</label>
              </td>
              <td>
                <input
                  type="checkbox"
                  id="shared"
                  autoComplete="off"
                  onChange={(e) => setShared(e.target.checked)}
                  checked={shared}
                />
              </td>
            </tr>
          </tbody>
        </table>

        <div className="create-button">
          <button disabled={!month || !category || !!errMsg}>Create</button>
        </div>
      </form>
    </>
  );
}

export default CreateBudgetForm;

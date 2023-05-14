import { FormEvent, useEffect, useState } from "react";
import { createBudget, fetchBudgets } from "../../services/BudgetService";
import useAuth from "../../hooks/useAuth";
import BudgetTable from "./BudgetTable";
import "./BudgetTable.css";
import { AxiosResponse } from "axios";

export type Budget = {
  month: string;
  category: string;
  income: number;
  expenses: number;
  netIncome: number;
  shared: boolean;
};

type User = {
  id: string;
  email: string;
};

type UserBudgets = {
  user: User;
  budgets: Budget[];
};

const Budgets = () => {
  const { auth } = useAuth();
  const [budgets, setBudgets] = useState<UserBudgets[]>([]);
  const [errMsg, setErrMsg] = useState("");
  const [createNew, setCreateNew] = useState<boolean>(false);
  const [month, setMonth] = useState("");
  const [category, setCategory] = useState("");
  const [income, setIncome] = useState(0);
  const [expenses, setExpenses] = useState(0);
  const [shared, setShared] = useState(false);

  const [selectedUser, setSelectedUser] = useState<UserBudgets | undefined>();

  const handleUserChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
    const userEmail = event.target.value;
    const selectedBudgetsData = budgets.find(
      (budgets) => budgets.user.email === userEmail
    );

    if (selectedBudgetsData) {
      setSelectedUser(selectedBudgetsData);
    } else {
      setSelectedUser(undefined);
    }
  };

  useEffect(() => {
    const requestController = new AbortController();
    const getBudgets = async () => {
      try {
        let response: AxiosResponse<UserBudgets[], any> = await fetchBudgets(
          auth.accessToken,
          requestController
        );
        setBudgets(response?.data);
        const myBudget = response?.data.find(
          (x) => x.user.email === auth.email
        );
        setSelectedUser(myBudget);
      } catch (error) {
        console.error(error);
      }
    };

    getBudgets();

    return () => {
      requestController.abort();
    };
  }, []);

  const handleSubmit = async (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    try {
      await createBudget(
        month,
        category,
        income,
        expenses,
        shared,
        auth.accessToken
      );

      setBudgets((prev) => {
        const updatedBudgets = prev.map((budgetsData) => {
          if (budgetsData.user.email === auth.email) {
            const newBudget: Budget = {
              month: month,
              category: category,
              income: income,
              expenses: expenses,
              netIncome: income - expenses,
              shared: shared,
            };
            const updatedBudget = {
              ...budgetsData,
              budgets: [...budgetsData.budgets, newBudget],
            };

            setSelectedUser(updatedBudget);
            return updatedBudget;
          }
          return budgetsData;
        });

        return updatedBudgets;
      });

      setMonth("");
      setCategory("");
      setIncome(0);
      setExpenses(0);
      setShared(false);
      setCreateNew(false);
    } catch (err) {
      setErrMsg("Error has occured");
    }
  };

  const selectedUserEmail = selectedUser?.user.email;
  const onCancelClicked = () => {
    setErrMsg("");
    setMonth("");
    setCategory("");
    setIncome(0);
    setExpenses(0);
    setShared(false);
    setCreateNew(false);
  };

  const renderSelectUser = () => (
    <div>
      <label htmlFor="user-select">Select User: </label>
      <select
        id="user-select"
        value={selectedUserEmail || ""}
        onChange={handleUserChange}
      >
        {budgets.map((budgets) => {
          const userEmail = budgets.user.email;

          return (
            <option key={userEmail} value={userEmail}>
              {userEmail}
            </option>
          );
        })}
      </select>
    </div>
  );

  return (
    <>
      {renderSelectUser()}

      <div>
        <h1>
          Budgets for{" "}
          {selectedUserEmail === auth.email ? "myselft" : selectedUserEmail}
        </h1>
        {createNew ? (
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
                      <input
                        type="text"
                        id="category"
                        autoComplete="off"
                        onChange={(e) => setCategory(e.target.value)}
                        value={category}
                        required
                      />
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
                <button disabled={!month || !category || !!errMsg}>
                  Create
                </button>
              </div>
            </form>
          </>
        ) : (
          <>
            {auth.email === selectedUserEmail && (
              <button onClick={() => setCreateNew(true)}>Create</button>
            )}
            <div className="table-container">
              {selectedUser && <BudgetTable budgets={selectedUser.budgets} />}
            </div>
          </>
        )}
      </div>
    </>
  );
};

export default Budgets;

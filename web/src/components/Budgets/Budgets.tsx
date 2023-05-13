import { FormEvent, useEffect, useState } from "react";
import { createBudget, fetchBudgets } from "../../services/BudgetService";
import useAuth from "../../hooks/useAuth";
import BudgetTable from "./BudgetTable";
import "./BudgetTable.css";

export type Budget = {
  month: string;
  category: string;
  income: number;
  expenses: number;
  netIncome: number;
};

const Budgets = () => {
  const { auth } = useAuth();
  const [budgets, setBudgets] = useState<Array<Budget>>([]);
  const [createNew, setCreateNew] = useState<boolean>(false);
  const [month, setMonth] = useState("");
  const [category, setCategory] = useState("");
  const [income, setIncome] = useState(0);
  const [expenses, setExpenses] = useState(0);

  useEffect(() => {
    const requestController = new AbortController();

    const getBudgets = async () => {
      try {
        let response = await fetchBudgets(auth.accessToken, requestController);
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

  const handleSubmit = async (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    try {
      await createBudget(month, income, expenses, auth.accessToken);

      setBudgets((prev) => [
        ...prev,
        {
          month: month,
          category: category,
          income: income,
          expenses: expenses,
          netIncome: income - expenses,
        },
      ]);

      setMonth("");
      setCategory("");
      setIncome(0);
      setExpenses(0);
      setCreateNew(false);
    } catch (err) {
      //   if (isAxiosError(err) && !err?.response) {
      //     setErrMsg("No Server Response");
      //   } else if (isAxiosError(err) && err.response?.status === 400) {
      //     setErrMsg("Missing Username or Password");
      //   } else if (isAxiosError(err) && err.response?.status === 401) {
      //     setErrMsg("Unauthorized");
      //   } else {
      //     setErrMsg("Login Failed");
      //   }
      //   errRef?.current?.focus();
    }
  };

  return (
    <>
      <div className="container">
        <h1>Budgets</h1>
        {createNew ? (
          <>
            <button onClick={() => setCreateNew(false)}>Cancel</button>
            <form onSubmit={handleSubmit}>
              <label htmlFor="month">Month:</label>
              <input
                type="text"
                id="month"
                autoComplete="off"
                onChange={(e) => setMonth(e.target.value)}
                value={month}
                required
              />
              <label htmlFor="category">Category:</label>
              <input
                type="text"
                id="category"
                autoComplete="off"
                onChange={(e) => setCategory(e.target.value)}
                value={category}
                required
              />
              <label htmlFor="income">Income:</label>
              <input
                type="number"
                id="income"
                autoComplete="off"
                onChange={(e) => setIncome(parseInt(e.target.value))}
                value={income}
                required
              />
              <label htmlFor="expenses">Expenses:</label>
              <input
                type="number"
                id="expenses"
                autoComplete="off"
                className="input-field"
                onChange={(e) => setExpenses(parseInt(e.target.value))}
                value={expenses}
                required
              />
              <button>Create</button>
            </form>
          </>
        ) : (
          <>
            <button onClick={() => setCreateNew(true)}>Create</button>
            <div className="table-container">
              <BudgetTable budgets={budgets} />
            </div>
          </>
        )}
      </div>
    </>
  );
};

export default Budgets;

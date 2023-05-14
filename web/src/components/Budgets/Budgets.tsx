import { useEffect, useState } from "react";
import useAuth from "../../hooks/useAuth";
import { AxiosResponse } from "axios";
import BudgetTable from "./BudgetTable";
import { fetchBudgets, fetchCategories } from "../../services/BudgetService";
import CreateBudgetForm from "./CreateBudgetForm";
import { Budget, Category, UserBudgets } from "../../types/componentTypes";

import "./Budgets.css";

const Budgets = () => {
  const { auth } = useAuth();
  const [budgets, setBudgets] = useState<UserBudgets[]>([]);
  const [categories, setCategories] = useState<Record<number, string>>({});

  const [createNew, setCreateNew] = useState<boolean>(false);

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
    const getCategories = async () => {
      try {
        let response: AxiosResponse<Category[], any> = await fetchCategories(
          auth.accessToken,
          requestController
        );

        const categoriesNameById: Record<number, string> = response.data.reduce(
          (agg, curr) => {
            agg[curr.id] = curr.name;
            return agg;
          },
          {} as Record<number, string>
        );

        setCategories(categoriesNameById);
      } catch (error) {
        console.error(error);
      }
    };

    getCategories();
    getBudgets();

    return () => {
      requestController.abort();
    };
  }, []);

  const createNewBudget = (newBudget: Budget) => {
    setBudgets((prev) => {
      const updatedBudgets = prev.map((budgetsData) => {
        if (budgetsData.user.email === auth.email) {
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

    setCreateNew(false);
  };

  const selectedUserEmail = selectedUser?.user.email;
  const isMe = selectedUserEmail === auth.email;

  const RenderUsersDropdown = () => (
    <div>
      <label htmlFor="user-select">Select User: </label>
      <select
        id="user-select"
        value={selectedUserEmail || ""}
        onChange={handleUserChange}
      >
        {budgets.map((userBudgets) => {
          const userEmail = userBudgets.user.email;

          return (
            <option key={userEmail} value={userEmail}>
              {userEmail}
            </option>
          );
        })}
      </select>
    </div>
  );

  const BudgetsForLabel = () => (
    <h2>
      {`Budgets for: `}
      {isMe ? "me" : selectedUserEmail}
    </h2>
  );

  return (
    <>
      <RenderUsersDropdown />
      <div>
        <BudgetsForLabel />
        {createNew ? (
          <CreateBudgetForm
            onCancel={() => setCreateNew(false)}
            onCreate={createNewBudget}
            categories={categories}
          />
        ) : (
          <>
            {auth.email === selectedUserEmail && (
              <button onClick={() => setCreateNew(true)}>Create</button>
            )}
            <div className="table-container">
              {selectedUser && (
                <BudgetTable
                  budgets={selectedUser.budgets}
                  categories={categories}
                />
              )}
            </div>
          </>
        )}
      </div>
    </>
  );
};

export default Budgets;

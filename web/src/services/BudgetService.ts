import { AxiosResponse } from "axios";
import axios from "../api/axios";
import { CREATE_BUDGET, FETCH_BUDGETS } from "../utils/navigation";

export const fetchBudgets = async (
  token: string | undefined,
  controller: AbortController
) => {
  try {
    const response: AxiosResponse = await axios.get(FETCH_BUDGETS, {
      headers: {
        "Content-Type": "application/json",
        ...(token ? { Authorization: `Bearer ${token}` } : {}),
      },
      withCredentials: true,
      signal: controller.signal,
    });

    return response;
  } catch (err) {
    throw err;
  }
};

export const createBudget = async (
  month: string,
  category: string,
  income: number,
  expenses: number,
  shared: boolean,
  token: string | undefined
) => {
  try {
    const response: AxiosResponse = await axios.post(
      CREATE_BUDGET,
      JSON.stringify({ month, category, income, expenses, shared }),
      {
        headers: {
          "Content-Type": "application/json",
          ...(token ? { Authorization: `Bearer ${token}` } : {}),
        },
        withCredentials: true,
      }
    );

    return response;
  } catch (err) {
    throw err;
  }
};

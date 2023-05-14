import { AxiosResponse } from "axios";
import axios from "../api/axios";
import {
  CREATE_BUDGET,
  FETCH_BUDGETS,
  FETCH_CATEGORIES,
} from "../utils/navigation";
import { CreateNewBudget } from "../types/componentTypes";

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

export const fetchCategories = async (
  token: string | undefined,
  controller: AbortController
) => {
  try {
    const response: AxiosResponse = await axios.get(FETCH_CATEGORIES, {
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
  newBudget: CreateNewBudget,
  token: string | undefined
) => {
  try {
    const response: AxiosResponse = await axios.post(
      CREATE_BUDGET,
      JSON.stringify(newBudget),
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

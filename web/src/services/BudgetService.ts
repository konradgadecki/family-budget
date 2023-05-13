import { AxiosResponse } from "axios";
import axios from "../api/axios";
import { FETCH_BUDGETS } from "../utils/navigation";

export const fetchBudgets = async (controller: AbortController) => {
  try {
    const response: AxiosResponse = await axios.get(FETCH_BUDGETS, {
      headers: { "Content-Type": "application/json" },
      withCredentials: true,
      signal: controller.signal,
    });

    return response;
  } catch (err) {
    throw err;
  }
};

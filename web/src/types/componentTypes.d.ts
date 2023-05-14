import { SortDirection } from "./utilsTypes";

export type CreateNewBudget = {
  month: string;
  categoryId: number;
  income: number;
  expenses: number;
  shared: boolean;
};

export type Budget = {
  month: string;
  categoryId: number;
  income: number;
  expenses: number;
  netIncome: number;
  shared: boolean;
};

export type User = {
  id: string;
  email: string;
};

export type UserBudgets = {
  user: User;
  budgets: Budget[];
};

export type Category = {
  id: number;
  name: string;
};

export type CreateBudgetFormProps = {
  onCancel: () => void;
  onCreate: (budget: Budget) => void;
  categories: Record<number, string>;
};

export type SortConfig = {
  key: keyof Budget;
  direction: SortDirection;
};

export type BudgetTableProps = {
  budgets: Array<Budget>;
  categories: Record<number, string>;
};

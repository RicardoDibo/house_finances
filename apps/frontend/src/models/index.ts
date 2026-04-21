export interface Person {
  id: string;
  name: string;
  age: number;
}

export interface Category {
  id: string;
  description: string;
  purpose: number; // 0=Expense, 1=Income, 2=Both
  purposeName: string;
}

export interface Transaction {
  id: string;
  description: string;
  amount: number;
  type: number; // 0=Expense, 1=Income
  typeName: string;
  categoryId: string;
  categoryDescription: string;
  personId: string;
  personName: string;
  userId: string | null;
}

export interface PersonTotals {
  id: string;
  name: string;
  totalIncome: number;
  totalExpenses: number;
  balance: number;
}

export interface PersonTotalsSummary {
  persons: PersonTotals[];
  totalIncome: number;
  totalExpenses: number;
  balance: number;
}

export interface CategoryTotals {
  id: string;
  description: string;
  totalIncome: number;
  totalExpenses: number;
  balance: number;
}

export interface CategoryTotalsSummary {
  categories: CategoryTotals[];
  totalIncome: number;
  totalExpenses: number;
  balance: number;
}

export type UserRole = 'Admin' | 'User';

export interface AuthUser {
  id: string;
  name: string;
  email: string;
  role: number;
  roleName: UserRole;
}

export interface AuthResponse {
  token: string;
  user: AuthUser;
}

import axios from 'axios';
import type {
  Category,
  Transaction,
  UserTotalsSummary,
  CategoryTotalsSummary,
  AuthResponse,
  AuthUser,
} from '../models';

const api = axios.create({ baseURL: '/api' });

api.interceptors.request.use(config => {
  const token = localStorage.getItem('token');
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

api.interceptors.response.use(
  r => r,
  error => {
    if (error.response?.status === 401) {
      localStorage.removeItem('token');
      localStorage.removeItem('user');
      globalThis.location.href = '/login';
    }
    return Promise.reject(error);
  }
);

// Auth
export const login = (data: { email: string; password: string }) =>
  api.post<AuthResponse>('/auth/login', data).then(r => r.data);

// Users (Admin only)
export const getUsers = () => api.get<AuthUser[]>('/users').then(r => r.data);
export const createUser = (data: { name: string; email: string; password: string; role: number }) =>
  api.post<AuthUser>('/users', data).then(r => r.data);

// Categories
export const getCategories = () => api.get<Category[]>('/categories').then(r => r.data);
export const createCategory = (data: { description: string; purpose: number }) =>
  api.post<Category>('/categories', data).then(r => r.data);

// Transactions
export const getTransactions = () =>
  api.get<Transaction[]>('/transactions').then(r => r.data);
export const createTransaction = (data: {
  description: string;
  amount: number;
  type: number;
  categoryId: string;
}) => api.post<Transaction>('/transactions', data).then(r => r.data);

// Totals
export const getUserTotals = () =>
  api.get<UserTotalsSummary>('/totals/users').then(r => r.data);
export const getCategoryTotals = () =>
  api.get<CategoryTotalsSummary>('/totals/categories').then(r => r.data);

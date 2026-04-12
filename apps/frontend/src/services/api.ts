import axios from 'axios';
import type {
  Person,
  Category,
  Transaction,
  PersonTotalsSummary,
  CategoryTotalsSummary,
} from '../models';

const api = axios.create({ baseURL: '/api' });

// Persons
export const getPersons = () => api.get<Person[]>('/persons').then(r => r.data);
export const createPerson = (data: { name: string; age: number }) =>
  api.post<Person>('/persons', data).then(r => r.data);
export const updatePerson = (id: string, data: { name: string; age: number }) =>
  api.put<Person>(`/persons/${id}`, data).then(r => r.data);
export const deletePerson = (id: string) => api.delete(`/persons/${id}`);

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
  personId: string;
}) => api.post<Transaction>('/transactions', data).then(r => r.data);

// Totals
export const getPersonTotals = () =>
  api.get<PersonTotalsSummary>('/totals/persons').then(r => r.data);
export const getCategoryTotals = () =>
  api.get<CategoryTotalsSummary>('/totals/categories').then(r => r.data);

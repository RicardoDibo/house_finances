import { useState, useEffect, useCallback } from 'react';
import { getTransactions } from '../services/api';
import type { Transaction } from '../models';

export function useTransactions() {
  const [transactions, setTransactions] = useState<Transaction[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');

  const reload = useCallback(async () => {
    setLoading(true);
    setError('');
    try {
      setTransactions(await getTransactions());
    } catch {
      setError('Erro ao carregar transações.');
    } finally {
      setLoading(false);
    }
  }, []);

  useEffect(() => { reload(); }, [reload]);

  return { transactions, loading, error, reload };
}

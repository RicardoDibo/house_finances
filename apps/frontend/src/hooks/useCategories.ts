import { useState, useEffect, useCallback } from 'react';
import { getCategories } from '../services/api';
import type { Category } from '../models';

export function useCategories() {
  const [categories, setCategories] = useState<Category[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');

  const reload = useCallback(async () => {
    setLoading(true);
    setError('');
    try {
      setCategories(await getCategories());
    } catch {
      setError('Erro ao carregar categorias.');
    } finally {
      setLoading(false);
    }
  }, []);

  useEffect(() => { reload(); }, [reload]);

  return { categories, loading, error, reload };
}

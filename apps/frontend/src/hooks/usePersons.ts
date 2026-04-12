import { useState, useEffect, useCallback } from 'react';
import { getPersons } from '../services/api';
import type { Person } from '../models';

export function usePersons() {
  const [persons, setPersons] = useState<Person[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');

  const reload = useCallback(async () => {
    setLoading(true);
    setError('');
    try {
      setPersons(await getPersons());
    } catch {
      setError('Erro ao carregar pessoas.');
    } finally {
      setLoading(false);
    }
  }, []);

  useEffect(() => { reload(); }, [reload]);

  return { persons, loading, error, reload };
}

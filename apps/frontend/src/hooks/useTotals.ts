import { useState, useEffect } from 'react';
import { getPersonTotals, getCategoryTotals } from '../services/api';
import type { PersonTotalsSummary, CategoryTotalsSummary } from '../models';

export function useTotals() {
  const [personData, setPersonData] = useState<PersonTotalsSummary | null>(null);
  const [categoryData, setCategoryData] = useState<CategoryTotalsSummary | null>(null);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    setLoading(true);
    Promise.all([
      getPersonTotals().then(setPersonData),
      getCategoryTotals().then(setCategoryData),
    ]).finally(() => setLoading(false));
  }, []);

  return { personData, categoryData, loading };
}

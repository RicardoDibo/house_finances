import { useState, useEffect } from 'react';
import { getUserTotals, getCategoryTotals } from '../services/api';
import type { UserTotalsSummary, CategoryTotalsSummary } from '../models';

export function useTotals() {
  const [userData, setUserData] = useState<UserTotalsSummary | null>(null);
  const [categoryData, setCategoryData] = useState<CategoryTotalsSummary | null>(null);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    setLoading(true);
    Promise.all([
      getUserTotals().then(setUserData),
      getCategoryTotals().then(setCategoryData),
    ]).finally(() => setLoading(false));
  }, []);

  return { userData, categoryData, loading };
}

import { useState } from 'react';
import { createCategory } from '../services/api';
import { useCategories } from '../hooks/useCategories';

const PURPOSE_OPTIONS = [
  { value: 0, label: 'Despesa' },
  { value: 1, label: 'Receita' },
  { value: 2, label: 'Ambas' },
];

const purposeLabel = (p: number) =>
  PURPOSE_OPTIONS.find(o => o.value === p)?.label ?? String(p);

const purposeBadge = (p: number) => {
  if (p === 0) return 'bg-red-100 text-red-700';
  if (p === 1) return 'bg-green-100 text-green-700';
  return 'bg-blue-100 text-blue-700';
};

export default function CategoriesPage() {
  const { categories, reload } = useCategories();
  const [form, setForm] = useState({ description: '', purpose: '2' });
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError('');
    if (!form.description.trim()) return setError('Descrição é obrigatória.');

    setLoading(true);
    try {
      await createCategory({ description: form.description.trim(), purpose: parseInt(form.purpose) });
      setForm({ description: '', purpose: '2' });
      await reload();
    } catch (err: any) {
      setError(err.response?.data?.title || 'Erro ao salvar.');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div>
      <h1 className="text-2xl font-bold text-gray-800 mb-6">Categorias</h1>

      <div className="bg-white rounded-lg border border-gray-200 p-6 mb-6 shadow-sm">
        <h2 className="text-base font-semibold text-gray-700 mb-4">Nova Categoria</h2>
        <form onSubmit={handleSubmit} className="flex flex-wrap gap-3 items-end">
          <div className="flex flex-col gap-1">
            <label className="text-xs font-medium text-gray-500 uppercase tracking-wide">
              Descrição
            </label>
            <input
              className="border border-gray-300 rounded px-3 py-2 text-sm w-80 focus:outline-none focus:ring-2 focus:ring-blue-400"
              placeholder="Descrição da categoria"
              maxLength={400}
              value={form.description}
              onChange={e => setForm(f => ({ ...f, description: e.target.value }))}
            />
          </div>
          <div className="flex flex-col gap-1">
            <label className="text-xs font-medium text-gray-500 uppercase tracking-wide">
              Finalidade
            </label>
            <select
              className="border border-gray-300 rounded px-3 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-blue-400"
              value={form.purpose}
              onChange={e => setForm(f => ({ ...f, purpose: e.target.value }))}
            >
              {PURPOSE_OPTIONS.map(o => (
                <option key={o.value} value={o.value}>{o.label}</option>
              ))}
            </select>
          </div>
          <button
            type="submit"
            disabled={loading}
            className="bg-blue-600 hover:bg-blue-700 text-white px-4 py-2 rounded text-sm font-medium disabled:opacity-50 transition-colors"
          >
            {loading ? 'Salvando...' : 'Adicionar'}
          </button>
        </form>
        {error && <p className="mt-3 text-sm text-red-600">{error}</p>}
      </div>

      <div className="bg-white rounded-lg border border-gray-200 shadow-sm overflow-hidden">
        <table className="w-full text-sm">
          <thead>
            <tr className="bg-gray-50 border-b border-gray-200">
              <th className="text-left px-4 py-3 font-semibold text-gray-600">Descrição</th>
              <th className="text-left px-4 py-3 font-semibold text-gray-600">Finalidade</th>
            </tr>
          </thead>
          <tbody>
            {categories.length === 0 && (
              <tr>
                <td colSpan={2} className="text-center py-8 text-gray-400">
                  Nenhuma categoria cadastrada.
                </td>
              </tr>
            )}
            {categories.map(c => (
              <tr key={c.id} className="border-b border-gray-100 hover:bg-gray-50">
                <td className="px-4 py-3 text-gray-800">{c.description}</td>
                <td className="px-4 py-3">
                  <span className={`text-xs font-medium px-2 py-0.5 rounded ${purposeBadge(c.purpose)}`}>
                    {purposeLabel(c.purpose)}
                  </span>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}

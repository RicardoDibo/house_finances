import { useState } from 'react';
import { createTransaction } from '../services/api';
import { usePersons } from '../hooks/usePersons';
import { useCategories } from '../hooks/useCategories';
import { useTransactions } from '../hooks/useTransactions';

const TYPE_OPTIONS = [
  { value: 0, label: 'Despesa' },
  { value: 1, label: 'Receita' },
];

const emptyForm = {
  description: '',
  amount: '',
  type: '0',
  categoryId: '',
  personId: '',
};

const formatCurrency = (v: number) =>
  v.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });

export default function TransactionsPage() {
  const { transactions, reload: reloadTransactions } = useTransactions();
  const { persons } = usePersons();
  const { categories } = useCategories();
  const [form, setForm] = useState(emptyForm);
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);

  const selectedType = Number.parseInt(form.type);
  const compatibleCategories = categories.filter(
    c => c.purpose === 2 || c.purpose === selectedType
  );

  const handleTypeChange = (type: string) => {
    setForm(f => ({ ...f, type, categoryId: '' }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError('');
    const amount = Number.parseFloat(form.amount);
    if (!form.description.trim()) return setError('Descrição é obrigatória.');
    if (Number.isNaN(amount) || amount <= 0) return setError('Valor deve ser positivo.');
    if (!form.personId) return setError('Selecione uma pessoa.');
    if (!form.categoryId) return setError('Selecione uma categoria.');

    setLoading(true);
    try {
      await createTransaction({
        description: form.description.trim(),
        amount,
        type: selectedType,
        categoryId: form.categoryId,
        personId: form.personId,
      });
      setForm(emptyForm);
      await reloadTransactions();
    } catch (err: any) {
      setError(err.response?.data?.title || 'Erro ao salvar.');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div>
      <h1 className="text-2xl font-bold text-gray-800 mb-6">Transações</h1>

      <div className="bg-white rounded-lg border border-gray-200 p-6 mb-6 shadow-sm">
        <h2 className="text-base font-semibold text-gray-700 mb-4">Nova Transação</h2>
        <form onSubmit={handleSubmit} className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4">
          <div className="flex flex-col gap-1 lg:col-span-2">
            <label htmlFor="tx-description" className="text-xs font-medium text-gray-500 uppercase tracking-wide">Descrição</label>
            <input
              id="tx-description"
              className="border border-gray-300 rounded px-3 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-blue-400"
              placeholder="Descrição da transação"
              maxLength={400}
              value={form.description}
              onChange={e => setForm(f => ({ ...f, description: e.target.value }))}
            />
          </div>

          <div className="flex flex-col gap-1">
            <label htmlFor="tx-amount" className="text-xs font-medium text-gray-500 uppercase tracking-wide">Valor (R$)</label>
            <input
              id="tx-amount"
              type="number" min="0.01" step="0.01"
              className="border border-gray-300 rounded px-3 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-blue-400"
              placeholder="0,00"
              value={form.amount}
              onChange={e => setForm(f => ({ ...f, amount: e.target.value }))}
            />
          </div>

          <div className="flex flex-col gap-1">
            <label htmlFor="tx-type" className="text-xs font-medium text-gray-500 uppercase tracking-wide">Tipo</label>
            <select
              id="tx-type"
              className="border border-gray-300 rounded px-3 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-blue-400"
              value={form.type}
              onChange={e => handleTypeChange(e.target.value)}
            >
              {TYPE_OPTIONS.map(o => (
                <option key={o.value} value={o.value}>{o.label}</option>
              ))}
            </select>
          </div>

          <div className="flex flex-col gap-1">
            <label htmlFor="tx-category" className="text-xs font-medium text-gray-500 uppercase tracking-wide">Categoria</label>
            <select
              id="tx-category"
              className="border border-gray-300 rounded px-3 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-blue-400"
              value={form.categoryId}
              onChange={e => setForm(f => ({ ...f, categoryId: e.target.value }))}
            >
              <option value="">Selecione...</option>
              {compatibleCategories.map(c => (
                <option key={c.id} value={c.id}>{c.description} ({c.purposeName})</option>
              ))}
            </select>
          </div>

          <div className="flex flex-col gap-1">
            <label htmlFor="tx-person" className="text-xs font-medium text-gray-500 uppercase tracking-wide">Pessoa</label>
            <select
              id="tx-person"
              className="border border-gray-300 rounded px-3 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-blue-400"
              value={form.personId}
              onChange={e => setForm(f => ({ ...f, personId: e.target.value }))}
            >
              <option value="">Selecione...</option>
              {persons.map(p => (
                <option key={p.id} value={p.id}>
                  {p.name} ({p.age} anos{p.age < 18 ? ' - menor' : ''})
                </option>
              ))}
            </select>
          </div>

          <div className="flex items-end">
            <button
              type="submit"
              disabled={loading}
              className="w-full bg-blue-600 hover:bg-blue-700 text-white px-4 py-2 rounded text-sm font-medium disabled:opacity-50 transition-colors"
            >
              {loading ? 'Salvando...' : 'Adicionar'}
            </button>
          </div>
        </form>
        {error && <p className="mt-3 text-sm text-red-600">{error}</p>}
      </div>

      <div className="bg-white rounded-lg border border-gray-200 shadow-sm overflow-hidden">
        <table className="w-full text-sm">
          <thead>
            <tr className="bg-gray-50 border-b border-gray-200">
              <th className="text-left px-4 py-3 font-semibold text-gray-600">Descrição</th>
              <th className="text-left px-4 py-3 font-semibold text-gray-600">Pessoa</th>
              <th className="text-left px-4 py-3 font-semibold text-gray-600">Categoria</th>
              <th className="text-left px-4 py-3 font-semibold text-gray-600">Tipo</th>
              <th className="text-right px-4 py-3 font-semibold text-gray-600">Valor</th>
            </tr>
          </thead>
          <tbody>
            {transactions.length === 0 && (
              <tr>
                <td colSpan={5} className="text-center py-8 text-gray-400">
                  Nenhuma transação cadastrada.
                </td>
              </tr>
            )}
            {transactions.map(t => (
              <tr key={t.id} className="border-b border-gray-100 hover:bg-gray-50">
                <td className="px-4 py-3 text-gray-800">{t.description}</td>
                <td className="px-4 py-3 text-gray-600">{t.personName}</td>
                <td className="px-4 py-3 text-gray-600">{t.categoryDescription}</td>
                <td className="px-4 py-3">
                  <span className={`text-xs font-medium px-2 py-0.5 rounded ${
                    t.type === 1 ? 'bg-green-100 text-green-700' : 'bg-red-100 text-red-700'
                  }`}>
                    {t.type === 1 ? 'Receita' : 'Despesa'}
                  </span>
                </td>
                <td className={`px-4 py-3 text-right font-medium ${
                  t.type === 1 ? 'text-green-700' : 'text-red-600'
                }`}>
                  {formatCurrency(t.amount)}
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}

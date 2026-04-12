import { useState } from 'react';
import { createPerson, updatePerson, deletePerson } from '../services/api';
import { usePersons } from '../hooks/usePersons';
import type { Person } from '../models';

const emptyForm = { name: '', age: '' };

export default function PersonsPage() {
  const { persons, reload } = usePersons();
  const [form, setForm] = useState(emptyForm);
  const [editing, setEditing] = useState<Person | null>(null);
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError('');
    const age = Number.parseInt(form.age);
    if (!form.name.trim()) return setError('Nome é obrigatório.');
    if (Number.isNaN(age) || age < 0) return setError('Idade inválida.');

    setLoading(true);
    try {
      if (editing) {
        await updatePerson(editing.id, { name: form.name.trim(), age });
      } else {
        await createPerson({ name: form.name.trim(), age });
      }
      setForm(emptyForm);
      setEditing(null);
      await reload();
    } catch (err: any) {
      setError(err.response?.data?.title || 'Erro ao salvar.');
    } finally {
      setLoading(false);
    }
  };

  const handleEdit = (p: Person) => {
    setEditing(p);
    setForm({ name: p.name, age: String(p.age) });
    setError('');
  };

  const handleDelete = async (id: string) => {
    if (!confirm('Excluir esta pessoa e todas as suas transações?')) return;
    try {
      await deletePerson(id);
      await reload();
    } catch {
      setError('Erro ao excluir.');
    }
  };

  const handleCancel = () => {
    setEditing(null);
    setForm(emptyForm);
    setError('');
  };

  let submitButtonText = 'Adicionar';
  if (loading) submitButtonText = 'Salvando...';
  else if (editing) submitButtonText = 'Salvar';

  return (
    <div>
      <h1 className="text-2xl font-bold text-gray-800 mb-6">Pessoas</h1>

      <div className="bg-white rounded-lg border border-gray-200 p-6 mb-6 shadow-sm">
        <h2 className="text-base font-semibold text-gray-700 mb-4">
          {editing ? 'Editar Pessoa' : 'Nova Pessoa'}
        </h2>
        <form onSubmit={handleSubmit} className="flex flex-wrap gap-3 items-end">
          <div className="flex flex-col gap-1">
            <label htmlFor="name" className="text-xs font-medium text-gray-500 uppercase tracking-wide">
              Nome
            </label>
            <input
              id="name"
              className="border border-gray-300 rounded px-3 py-2 text-sm w-64 focus:outline-none focus:ring-2 focus:ring-blue-400"
              placeholder="Nome completo"
              maxLength={200}
              value={form.name}
              onChange={e => setForm(f => ({ ...f, name: e.target.value }))}
            />
          </div>
          <div className="flex flex-col gap-1">
            <label htmlFor="age" className="text-xs font-medium text-gray-500 uppercase tracking-wide">
              Idade
            </label>
            <input
              id="age"
              type="number"
              min={0}
              className="border border-gray-300 rounded px-3 py-2 text-sm w-24 focus:outline-none focus:ring-2 focus:ring-blue-400"
              placeholder="0"
              value={form.age}
              onChange={e => setForm(f => ({ ...f, age: e.target.value }))}
            />
          </div>
          <div className="flex gap-2">
            <button
              type="submit"
              disabled={loading}
              className="bg-blue-600 hover:bg-blue-700 text-white px-4 py-2 rounded text-sm font-medium disabled:opacity-50 transition-colors"
            >
              {submitButtonText}
            </button>
            {editing && (
              <button
                type="button"
                onClick={handleCancel}
                className="border border-gray-300 text-gray-600 hover:bg-gray-50 px-4 py-2 rounded text-sm font-medium transition-colors"
              >
                Cancelar
              </button>
            )}
          </div>
        </form>
        {error && <p className="mt-3 text-sm text-red-600">{error}</p>}
      </div>

      <div className="bg-white rounded-lg border border-gray-200 shadow-sm overflow-hidden">
        <table className="w-full text-sm">
          <thead>
            <tr className="bg-gray-50 border-b border-gray-200">
              <th className="text-left px-4 py-3 font-semibold text-gray-600">Nome</th>
              <th className="text-left px-4 py-3 font-semibold text-gray-600">Idade</th>
              <th className="px-4 py-3" />
            </tr>
          </thead>
          <tbody>
            {persons.length === 0 && (
              <tr>
                <td colSpan={3} className="text-center py-8 text-gray-400">
                  Nenhuma pessoa cadastrada.
                </td>
              </tr>
            )}
            {persons.map(p => (
              <tr key={p.id} className="border-b border-gray-100 hover:bg-gray-50">
                <td className="px-4 py-3 font-medium text-gray-800">{p.name}</td>
                <td className="px-4 py-3 text-gray-600">
                  {p.age}{p.age < 18 && (
                    <span className="ml-1 text-xs bg-amber-100 text-amber-700 px-1.5 py-0.5 rounded">
                      menor
                    </span>
                  )}
                </td>
                <td className="px-4 py-3 text-right">
                  <button onClick={() => handleEdit(p)} className="text-blue-600 hover:text-blue-800 text-xs font-medium mr-3">
                    Editar
                  </button>
                  <button onClick={() => handleDelete(p.id)} className="text-red-500 hover:text-red-700 text-xs font-medium">
                    Excluir
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}

import { useState } from 'react';
import { createUser, getUsers } from '../services/api';
import type { AuthUser } from '../models';
import { useEffect } from 'react';

const emptyForm = { name: '', email: '', password: '', role: 1 };

const roleLabel = (role: number) => (role === 0 ? 'Admin' : 'Usuário');

export default function RegisterUserPage() {
  const [users, setUsers] = useState<AuthUser[]>([]);
  const [form, setForm] = useState(emptyForm);
  const [error, setError] = useState('');
  const [success, setSuccess] = useState('');
  const [loading, setLoading] = useState(false);

  const loadUsers = async () => {
    try {
      setUsers(await getUsers());
    } catch {
      // non-critical
    }
  };

  useEffect(() => { loadUsers(); }, []);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError('');
    setSuccess('');
    if (!form.name.trim()) return setError('Nome é obrigatório.');
    if (!form.email.trim()) return setError('Email é obrigatório.');
    if (form.password.length < 6) return setError('Senha deve ter no mínimo 6 caracteres.');

    setLoading(true);
    try {
      await createUser(form);
      setSuccess(`Usuário "${form.name}" criado com sucesso.`);
      setForm(emptyForm);
      await loadUsers();
    } catch (err: unknown) {
      const msg = (err as { response?: { data?: { title?: string } } })?.response?.data?.title;
      setError(msg ?? 'Erro ao criar usuário.');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div>
      <h1 className="text-2xl font-bold text-gray-800 mb-6">Gerenciar Usuários</h1>

      <div className="bg-white rounded-lg border border-gray-200 p-6 mb-6 shadow-sm">
        <h2 className="text-base font-semibold text-gray-700 mb-4">Novo Usuário</h2>
        <form onSubmit={handleSubmit} className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-4 items-end">
          <div className="flex flex-col gap-1">
            <label htmlFor="u-name" className="text-xs font-medium text-gray-500 uppercase tracking-wide">Nome</label>
            <input
              id="u-name"
              className="border border-gray-300 rounded px-3 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-blue-400"
              placeholder="Nome completo"
              maxLength={200}
              value={form.name}
              onChange={e => setForm(f => ({ ...f, name: e.target.value }))}
            />
          </div>

          <div className="flex flex-col gap-1">
            <label htmlFor="u-email" className="text-xs font-medium text-gray-500 uppercase tracking-wide">Email</label>
            <input
              id="u-email"
              type="email"
              className="border border-gray-300 rounded px-3 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-blue-400"
              placeholder="email@exemplo.com"
              value={form.email}
              onChange={e => setForm(f => ({ ...f, email: e.target.value }))}
            />
          </div>

          <div className="flex flex-col gap-1">
            <label htmlFor="u-password" className="text-xs font-medium text-gray-500 uppercase tracking-wide">Senha</label>
            <input
              id="u-password"
              type="password"
              className="border border-gray-300 rounded px-3 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-blue-400"
              placeholder="Mínimo 6 caracteres"
              value={form.password}
              onChange={e => setForm(f => ({ ...f, password: e.target.value }))}
            />
          </div>

          <div className="flex flex-col gap-1">
            <label htmlFor="u-role" className="text-xs font-medium text-gray-500 uppercase tracking-wide">Perfil</label>
            <select
              id="u-role"
              className="border border-gray-300 rounded px-3 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-blue-400"
              value={form.role}
              onChange={e => setForm(f => ({ ...f, role: Number(e.target.value) }))}
            >
              <option value={1}>Usuário</option>
              <option value={0}>Admin</option>
            </select>
          </div>

          <div className="flex items-end sm:col-span-2 lg:col-span-4">
            <button
              type="submit"
              disabled={loading}
              className="bg-blue-600 hover:bg-blue-700 text-white px-4 py-2 rounded text-sm font-medium disabled:opacity-50 transition-colors"
            >
              {loading ? 'Criando...' : 'Criar Usuário'}
            </button>
          </div>
        </form>
        {error && <p className="mt-3 text-sm text-red-600">{error}</p>}
        {success && <p className="mt-3 text-sm text-green-600">{success}</p>}
      </div>

      <div className="bg-white rounded-lg border border-gray-200 shadow-sm overflow-hidden">
        <table className="w-full text-sm">
          <thead>
            <tr className="bg-gray-50 border-b border-gray-200">
              <th className="text-left px-4 py-3 font-semibold text-gray-600">Nome</th>
              <th className="text-left px-4 py-3 font-semibold text-gray-600">Email</th>
              <th className="text-left px-4 py-3 font-semibold text-gray-600">Perfil</th>
            </tr>
          </thead>
          <tbody>
            {users.length === 0 && (
              <tr>
                <td colSpan={3} className="text-center py-8 text-gray-400">
                  Nenhum usuário cadastrado.
                </td>
              </tr>
            )}
            {users.map(u => (
              <tr key={u.id} className="border-b border-gray-100 hover:bg-gray-50">
                <td className="px-4 py-3 font-medium text-gray-800">{u.name}</td>
                <td className="px-4 py-3 text-gray-600">{u.email}</td>
                <td className="px-4 py-3">
                  <span className={`text-xs font-medium px-2 py-0.5 rounded ${
                    u.roleName === 'Admin'
                      ? 'bg-purple-100 text-purple-700'
                      : 'bg-blue-100 text-blue-700'
                  }`}>
                    {roleLabel(u.role)}
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

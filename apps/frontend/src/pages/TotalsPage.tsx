import { useState } from 'react';
import { useTotals } from '../hooks/useTotals';

const formatCurrency = (v: number) =>
  v.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });

const balanceClass = (v: number) =>
  v >= 0 ? 'text-green-700 font-semibold' : 'text-red-600 font-semibold';

export default function TotalsPage() {
  const { personData, categoryData } = useTotals();
  const [tab, setTab] = useState<'persons' | 'categories'>('persons');

  return (
    <div>
      <h1 className="text-2xl font-bold text-gray-800 mb-6">Totais</h1>

      <div className="flex gap-2 mb-6">
        <button
          onClick={() => setTab('persons')}
          className={`px-4 py-2 rounded text-sm font-medium transition-colors ${
            tab === 'persons' ? 'bg-blue-600 text-white' : 'border border-gray-300 text-gray-600 hover:bg-gray-50'
          }`}
        >
          Por Pessoa
        </button>
        <button
          onClick={() => setTab('categories')}
          className={`px-4 py-2 rounded text-sm font-medium transition-colors ${
            tab === 'categories' ? 'bg-blue-600 text-white' : 'border border-gray-300 text-gray-600 hover:bg-gray-50'
          }`}
        >
          Por Categoria
        </button>
      </div>

      {tab === 'persons' && personData && (
        <TotalsTable
          rows={personData.persons.map(p => ({
            label: p.name,
            totalIncome: p.totalIncome,
            totalExpenses: p.totalExpenses,
            balance: p.balance,
          }))}
          summary={{ totalIncome: personData.totalIncome, totalExpenses: personData.totalExpenses, balance: personData.balance }}
          firstColLabel="Pessoa"
        />
      )}

      {tab === 'categories' && categoryData && (
        <TotalsTable
          rows={categoryData.categories.map(c => ({
            label: c.description,
            totalIncome: c.totalIncome,
            totalExpenses: c.totalExpenses,
            balance: c.balance,
          }))}
          summary={{ totalIncome: categoryData.totalIncome, totalExpenses: categoryData.totalExpenses, balance: categoryData.balance }}
          firstColLabel="Categoria"
        />
      )}
    </div>
  );
}

interface TotalsRow { label: string; totalIncome: number; totalExpenses: number; balance: number; }
interface TotalsTableProps { rows: TotalsRow[]; summary: { totalIncome: number; totalExpenses: number; balance: number }; firstColLabel: string; }

function TotalsTable({ rows, summary, firstColLabel }: TotalsTableProps) {
  if (rows.length === 0) {
    return (
      <div className="bg-white rounded-lg border border-gray-200 p-8 text-center text-gray-400 shadow-sm">
        Nenhum dado disponível.
      </div>
    );
  }

  return (
    <div className="bg-white rounded-lg border border-gray-200 shadow-sm overflow-hidden">
      <table className="w-full text-sm">
        <thead>
          <tr className="bg-gray-50 border-b border-gray-200">
            <th className="text-left px-4 py-3 font-semibold text-gray-600">{firstColLabel}</th>
            <th className="text-right px-4 py-3 font-semibold text-gray-600">Receitas</th>
            <th className="text-right px-4 py-3 font-semibold text-gray-600">Despesas</th>
            <th className="text-right px-4 py-3 font-semibold text-gray-600">Saldo</th>
          </tr>
        </thead>
        <tbody>
          {rows.map((row, i) => (
            <tr key={i} className="border-b border-gray-100 hover:bg-gray-50">
              <td className="px-4 py-3 font-medium text-gray-800">{row.label}</td>
              <td className="px-4 py-3 text-right text-green-700">{formatCurrency(row.totalIncome)}</td>
              <td className="px-4 py-3 text-right text-red-600">{formatCurrency(row.totalExpenses)}</td>
              <td className={`px-4 py-3 text-right ${balanceClass(row.balance)}`}>{formatCurrency(row.balance)}</td>
            </tr>
          ))}
        </tbody>
        <tfoot>
          <tr className="bg-gray-50 border-t-2 border-gray-300">
            <td className="px-4 py-3 font-bold text-gray-700">Total Geral</td>
            <td className="px-4 py-3 text-right font-bold text-green-700">{formatCurrency(summary.totalIncome)}</td>
            <td className="px-4 py-3 text-right font-bold text-red-600">{formatCurrency(summary.totalExpenses)}</td>
            <td className={`px-4 py-3 text-right text-base ${balanceClass(summary.balance)}`}>{formatCurrency(summary.balance)}</td>
          </tr>
        </tfoot>
      </table>
    </div>
  );
}

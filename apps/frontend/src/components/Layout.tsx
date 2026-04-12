import { NavLink, Outlet } from 'react-router-dom';

const navItems = [
  { to: '/persons', label: 'Pessoas' },
  { to: '/categories', label: 'Categorias' },
  { to: '/transactions', label: 'Transações' },
  { to: '/totals', label: 'Totais' },
];

export default function Layout() {
  return (
    <div className="min-h-screen bg-gray-50">
      <header className="bg-white border-b border-gray-200 shadow-sm">
        <div className="max-w-6xl mx-auto px-4 flex items-center gap-8 h-14">
          <span className="font-bold text-blue-700 text-lg tracking-tight">
            House Finances
          </span>
          <nav className="flex gap-1">
            {navItems.map(item => (
              <NavLink
                key={item.to}
                to={item.to}
                className={({ isActive }) =>
                  `px-3 py-1.5 rounded text-sm font-medium transition-colors ${
                    isActive
                      ? 'bg-blue-100 text-blue-700'
                      : 'text-gray-600 hover:bg-gray-100 hover:text-gray-900'
                  }`
                }
              >
                {item.label}
              </NavLink>
            ))}
          </nav>
        </div>
      </header>
      <main className="max-w-6xl mx-auto px-4 py-8">
        <Outlet />
      </main>
    </div>
  );
}

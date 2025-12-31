'use client';

import { useEffect, useState } from 'react';
import { AuthModal } from './AuthModal';
import { loginOrRegister, logout, getUserEmail, isAuthenticated } from '../lib/auth';

export default function HeaderOverlay() {
  const [showAuth, setShowAuth] = useState(false);
  const [user, setUser] = useState<string | null>(null);

  useEffect(() => {
    if (isAuthenticated()) {
      setUser(getUserEmail());
    }
  }, []);

  async function handleLogin(email: string, password: string) {
  try {
    await loginOrRegister(email, password);
    setUser(email);
    setShowAuth(false);
  } catch {
    alert('Не удалось войти или зарегистрироваться');
  }
}


  function handleLogout() {
    logout();
    setUser(null);
  }

  return (
    <>
      <div style={{
        position: 'absolute',
        top: 10,
        right: 10,
        zIndex: 1000,
      }}>
        {user ? (
          <div style={{ background: 'white', color: 'black', padding: 6, borderRadius: 4 }}>
            {user}
            <button onClick={handleLogout} style={{ marginLeft: 6 }}>
              Выйти
            </button>
          </div>
        ) : (
          <div style={{ background: 'white', color: 'black', padding: 6, borderRadius: 4 }}>
            <button onClick={() => setShowAuth(true)}>
              Войти
            </button>
          </div>
        )}
      </div>

      {showAuth && (
        <AuthModal
          onClose={() => setShowAuth(false)}
          onLogin={handleLogin}
        />
      )}
    </>
  );
}

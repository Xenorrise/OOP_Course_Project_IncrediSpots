'use client';

import { useState } from 'react';

type Props = {
  onClose: () => void;
  onLogin: (email: string, password: string) => void;
};

export function AuthModal({ onClose, onLogin }: Props) {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  function submit() {
    if (!email.trim() || !password.trim()) return;
    onLogin(email, password);
  }

  return (
    <div style={{
      position: 'fixed', top: 0, left: 0, width: '100%', height: '100%',
      backgroundColor: 'rgba(0,0,0,0.5)', display: 'flex', justifyContent: 'center', alignItems: 'center',
      zIndex: 1000
    }}>
      <div style={{ background: 'white', color: 'black', padding: 20, borderRadius: 8, minWidth: 250 }}>
        <h4>Вход / Регистрация</h4>
        <input
          placeholder="Email"
          value={email}
          onChange={e => setEmail(e.target.value)}
          style={{ width: '100%', marginBottom: 6 }}
        />
        <input
          placeholder="Пароль"
          type="password"
          value={password}
          onChange={e => setPassword(e.target.value)}
          style={{ width: '100%', marginBottom: 6 }}
        />
        <div style={{ display: 'flex', gap: 6 }}>
          <button onClick={submit}>Войти</button>
          <button onClick={onClose}>Отмена</button>
        </div>
      </div>
    </div>
  );
}

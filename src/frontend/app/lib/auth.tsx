const API_URL = process.env.NEXT_PUBLIC_API_URL ?? 'http://localhost:5183';

export async function register(email: string, password: string) {
  const res = await fetch(`${API_URL}/Auth/Register`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ email, password }),
  });

  if (!res.ok) {
    throw new Error('Register failed');
  }
}

export async function login(email: string, password: string) {
  const res = await fetch(`${API_URL}/Auth/Login`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ email, password }),
  });

  if (!res.ok) {
    throw new Error('Auth failed');
  }

  const data = await res.json();
  localStorage.setItem('token', data.token);
  localStorage.setItem('userEmail', email);
}

export async function loginOrRegister(email: string, password: string) {
  try {
    await login(email, password);
  } catch {
    await register(email, password);
    await login(email, password);
  }
}

export function logout() {
  localStorage.removeItem('token');
  localStorage.removeItem('userEmail');
}

export function getUserEmail() {
  return localStorage.getItem('userEmail');
}

export function isAuthenticated() {
  return !!localStorage.getItem('token');
}

export async function authFetch(input: RequestInfo, init?: RequestInit) {
  const res = await fetch(input, {
    ...init,
    headers: {
      ...init?.headers,
      Authorization: `Bearer ${localStorage.getItem('token')}`,
    },
  });

  if (res.status === 401) {
    logout();
    window.location.reload();
    throw new Error('Session expired');
  }

  return res;
}
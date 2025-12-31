import { SpotCreateDto } from '@/app/map/SpotForm';
import { authFetch } from './auth';

const API_URL = process.env.NEXT_PUBLIC_API_URL ?? 'http://localhost:5183';

export type SpotDto = {
  id: number;
  title: string;
  description: string;
  latitude: number;
  longitude: number;
  category: {
    id: number;
    name: string;
    emoji: string;
  } | null;
  rating: number;
  userVote?: 1 | -1 | 0;
};

export async function getComments(spotId: number) {
  const token = localStorage.getItem('token');
  const res = await authFetch(`${API_URL}/${spotId}/comments`, {
    method: 'GET',
    headers: { 'Content-Type': 'application/json', 
		Authorization: `Bearer ${token}`, } });
  return res.json();
}

export async function createComment(spotId: number, text: string) {
  const token = localStorage.getItem('token');

  const res = await authFetch(`${API_URL}/${spotId}/comments`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`,
    },
    body: JSON.stringify({ text }),
  });

  return res.json();
}

export async function getAllSpots(): Promise<SpotDto[]> {
  const res = await fetch(`${API_URL}/Spot/GetAll`);
  if (!res.ok) throw new Error('Failed to load spots');
  return res.json();
}

export async function createSpot(data: SpotCreateDto) {
	const token = localStorage.getItem('token');
	const searchRes = await authFetch(`${API_URL}/SpotCategory/GetByNameAndEmoji?name=${data.Category.Name}&emoji=${data.Category.Emoji}`, {
		method: 'GET',
		headers: { 'Content-Type': 'application/json' },
		});
	let categoryId: number;
	
	let existing = null;
	if (searchRes.ok) {
		const text = await searchRes.text(); 
		if (text) {
			existing = JSON.parse(text);
			categoryId = existing.id;
		}
	if (existing) {
		categoryId = existing.id;
	} else {
		// 2. создаём категорию
		const createCatRes = await authFetch(`${API_URL}/SpotCategory/Create`, {
		method: 'POST',
		headers: { 'Content-Type': 'application/json' },
		body: JSON.stringify(data.Category),
		});

		const createdCategory = await createCatRes.json();
		categoryId = createdCategory.id;
	}
	} else {
	throw new Error('Ошибка проверки категории');
	}
  const res = await authFetch(`${API_URL}/Spot/Create`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json', 
		Authorization: `Bearer ${token}`, },
    body: JSON.stringify({
      title: data.Title,
      description: data.Description,
	  categoryId: categoryId,
      latitude: data.Latitude,
      longitude: data.Longitude
    }),
  });

  if (!res.ok) {
    throw new Error('Ошибка при создании точки');
  }

  return res.json();
}

export async function voteSpot(
  spotId: number,
  value: 1 | -1
): Promise<{ rating: number; userVote: 1 | -1 | 0 }> {
  const token = localStorage.getItem('token');
  const res = await authFetch(`${API_URL}/Spot/Vote/${spotId}`, {
    method: 'POST',
    headers: {
      Authorization: `Bearer ${token}`,
      'Content-Type': 'application/json',
    },
    credentials: 'include',
    body: JSON.stringify({ value }),
  });

  if (!res.ok) {
    throw new Error('Vote failed');
  }

  return res.json();
}

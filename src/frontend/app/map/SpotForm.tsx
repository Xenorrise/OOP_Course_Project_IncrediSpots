'use client';

import { useState } from 'react';

export type CategoryDto = {
  name: string;
  emoji: string;
};

export type SpotCreateDto = {
  title: string;
  description: string;
  latitude: number;
  longitude: number;
  category: CategoryDto;
};

type Props = {
  lat: number;
  lng: number;
  onSubmit: (data: SpotCreateDto) => void;
  onCancel: () => void;
};

export function SpotForm({ lat, lng, onSubmit, onCancel }: Props) {
  const [title, setTitle] = useState('');
  const [description, setDescription] = useState('');
  const [categoryName, setCategoryName] = useState('');
  const [categoryEmoji, setCategoryEmoji] = useState('');
  const EMOJI_OPTIONS = [
  { emoji: '☕'},
  { emoji: '🍔'},
  { emoji: '🏞️'},
  { emoji: '🎓'},
  { emoji: '🏠'},
  { emoji: '🎮'},
	];	
	
  function submit() {
    if (!title.trim() || !categoryName.trim() || !categoryEmoji.trim()) return;

    onSubmit({
      title,
      description,
      latitude: lat,
      longitude: lng,
      category: {
        name: categoryName,
        emoji: categoryEmoji,
      },
    });
  }

  return (
    <div style={{ minWidth: 220 }}>
      <h4>Новая точка</h4>
	
      <input
        placeholder="Название точки"
        value={title}
        onChange={e => setTitle(e.target.value)}
        style={{ width: '100%', marginBottom: 6 }}
      />

      <textarea
        placeholder="Описание"
        value={description}
        onChange={e => setDescription(e.target.value)}
        style={{ width: '100%', marginBottom: 6 }}
      />

      <input
        placeholder="Категория (например: Кафе)"
        value={categoryName}
        onChange={e => setCategoryName(e.target.value)}
        style={{ width: '100%', marginBottom: 6 }}
      />
	<select
	value={categoryEmoji}
	onChange={e => setCategoryEmoji(e.target.value)}
	style={{ width: '25%', marginBottom: 6 }}
	>
	<option value="">Emoji</option>
	{EMOJI_OPTIONS.map(o => (
		<option key={o.emoji} value={o.emoji}>
		{o.emoji}
		</option>
	))}
	</select>
      <div style={{ display: 'flex', gap: 6 }}>
        <button onClick={submit}>Сохранить</button>
        <button onClick={onCancel}>Отмена</button>
      </div>
    </div>
  );
}

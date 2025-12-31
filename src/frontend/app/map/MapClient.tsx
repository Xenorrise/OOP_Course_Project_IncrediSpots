'use client';

import { MapContainer, TileLayer, useMapEvents } from 'react-leaflet';
import 'leaflet/dist/leaflet.css';
import { useState, useEffect } from 'react';
import { LatLng } from 'leaflet';
import { SpotPopup } from './SpotPopup';
import { getAllSpots, SpotDto, createSpot } from '../lib/api';
import { SpotViewPopup } from './SpotPopup';
import { SpotCreateDto } from './SpotForm';
import { isAuthenticated } from '../lib/auth';

function ClickHandler({
  onClick,
  disabled,
}: {
  onClick: (latlng: LatLng) => void;
  disabled: boolean;
}) {
  useMapEvents({
    click(e) {
      if (!disabled) onClick(e.latlng);
    },
  });
  return null;
}

export default function MapClient() {
  const [position, setPosition] = useState<LatLng | null>(null);
  const [lockClick, setLockClick] = useState(false);
  const [spots, setSpots] = useState<SpotDto[]>([]);

  useEffect(() => {
    getAllSpots().then(setSpots).catch(console.error);
  }, []);

  function closePopup() {
    setLockClick(true);
    setPosition(null);
    setTimeout(() => setLockClick(false), 0);
  }
  async function handleCreateSpot(data: SpotCreateDto) {
    if (!isAuthenticated()) {
      alert('Необходимо войти');
      return;
    }

    const created = await createSpot(data);
    setSpots(prev => [...prev, created]);
  }

  return (
    <MapContainer
      center={[55.751244, 37.618423]}
      zoom={13}
      style={{ height: '100%', width: '100%' }}
    >
      <TileLayer
        attribution="© OpenStreetMap"
        url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
      />

      {spots.map(s => (
        <SpotViewPopup key={s.id} spot={s} />
      ))}

      <ClickHandler
        onClick={setPosition}
        disabled={lockClick || !!position}
      />

      {position && (
        <SpotPopup
          lat={position.lat}
          lng={position.lng}
          onClose={closePopup}
          onCreated={handleCreateSpot}
        />
      )}
    </MapContainer>
  );
}

// app/map/MapClient.tsx
'use client';

import { MapContainer, TileLayer, useMapEvents } from 'react-leaflet';
import 'leaflet/dist/leaflet.css';
import { useState } from 'react';
import { LatLng } from 'leaflet';
import { SpotPopup } from './SpotPopup';

function ClickHandler({ onClick }: { onClick: (latlng: LatLng) => void }) {
  useMapEvents({
    click(e) {
      onClick(e.latlng);
    },
  });
  return null;
}

export default function MapClient() {
  const [position, setPosition] = useState<LatLng | null>(null);

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

      <ClickHandler onClick={setPosition} />

      {position && (
        <SpotPopup
          lat={position.lat}
          lng={position.lng}
          onClose={() => setPosition(null)}
        />
      )}
    </MapContainer>
  );
}

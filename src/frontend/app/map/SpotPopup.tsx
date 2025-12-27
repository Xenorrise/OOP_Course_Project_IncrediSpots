// app/map/SpotPopup.tsx
'use client';

import { Popup, Marker } from 'react-leaflet';
import { SpotForm, SpotCreateDto } from './SpotForm';
import L from 'leaflet';

type Props = {
  lat: number;
  lng: number;
  onClose: () => void;
};

const markerIcon = new L.Icon({
  iconUrl: 'https://unpkg.com/leaflet@1.9.4/dist/images/marker-icon.png',
  shadowUrl: 'https://unpkg.com/leaflet@1.9.4/dist/images/marker-shadow.png',
});

export function SpotPopup({ lat, lng, onClose }: Props) {
  function handleSubmit(data: SpotCreateDto) {
    console.log('Отправка на API:', data);
    onClose();
  }

  return (
    <Marker position={[lat, lng]} icon={markerIcon}>
      <Popup autoClose={false}
        closeOnClick={false}
        closeButton={false}>
        <SpotForm
          lat={lat}
          lng={lng}
          onSubmit={handleSubmit}
          onCancel={onClose}
        />
      </Popup>
    </Marker>
  );
}

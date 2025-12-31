'use client';

import dynamic from 'next/dynamic';
import HeaderOverlay from './Header';

const MapClient = dynamic(
  () => import('./MapClient'),
  { ssr: false }
);

export default function MapWrapper() {
  return (
    <div style={{ position: 'relative', height: '100%' }}>
      <MapClient />
      <HeaderOverlay />
    </div>
    );
}

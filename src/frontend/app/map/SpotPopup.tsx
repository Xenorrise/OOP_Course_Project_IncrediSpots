'use client';

import { Marker, Popup } from 'react-leaflet';
import { useEffect, useRef, useState } from 'react';
import { SpotForm, SpotCreateDto } from './SpotForm';
import L from 'leaflet';

type Props = {
  lat: number;
  lng: number;
  onClose: () => void;
  onCreated: (data: SpotCreateDto) => Promise<void>;
};

const markerIcon = new L.Icon({
  iconUrl: 'https://unpkg.com/leaflet@1.9.4/dist/images/marker-icon.png',
  shadowUrl: 'https://unpkg.com/leaflet@1.9.4/dist/images/marker-shadow.png',
});

export function SpotPopup({ lat, lng, onClose, onCreated }: Props) {
  const markerRef = useRef<L.Marker | null>(null);

  useEffect(() => {
    markerRef.current?.openPopup();
  }, []);

  async function handleSubmit(data: SpotCreateDto) {
    await onCreated(data); 
    onClose();
  }

  return (
    <Marker
      position={[lat, lng]}
      icon={markerIcon}
      ref={markerRef}
    >
      <Popup 
        autoClose={false}
        closeOnClick={false}
        closeButton={false}
      >
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

import { createComment, getComments, SpotDto, voteSpot } from '../lib/api';
import { CommentsList } from './CommentList';
import { AddCommentForm, CommentDto } from './AddCommentForm';
import { isAuthenticated } from '../lib/auth';
import { SpotVote } from './SpotVote';

const icon = new L.Icon({
  iconUrl: 'https://unpkg.com/leaflet@1.9.4/dist/images/marker-icon.png',
  shadowUrl: 'https://unpkg.com/leaflet@1.9.4/dist/images/marker-shadow.png',
});

export function SpotViewPopup({ spot }: { spot: SpotDto }) {
  const [comments, setComments] = useState<CommentDto[]>([]);
  const [rating, setRating] = useState(spot.rating);
  const [userVote, setUserVote] = useState<1 | -1 | 0>(spot.userVote ?? 0);
  
  useEffect(() => {
    getComments(spot.id).then(setComments);
  }, [spot.id]);

  async function handleAdd(text: string) {
    const created = await createComment(spot.id, text);
    setComments(prev => [...prev, created]);
  }

  async function handleVote(value: 1 | -1) {
    const result = await voteSpot(spot.id, value);
    setRating(result.rating);
    setUserVote(result.userVote);
  }

  return (
    <Marker position={[spot.latitude, spot.longitude]} icon={icon}>
      <Popup maxWidth={300}>
        <div style={{ width: 260 }}>
          <h4>
            {spot.category?.emoji} {spot.title}
          </h4>

          <p>{spot.description}</p>

          <SpotVote
            rating={rating}
            userVote={userVote}
            onVote={handleVote}
          />
          
          <div
            style={{
              maxHeight: 180,
              overflowY: 'auto',
              marginBottom: 8,
              paddingRight: 4,
            }}
          >
            <CommentsList comments={comments} />
          </div>

          {isAuthenticated() && (
            <AddCommentForm onSubmit={handleAdd} />
          )}
        </div>
      </Popup>
    </Marker>
  );
}



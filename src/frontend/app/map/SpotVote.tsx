type Props = {
  rating: number;
  userVote?: 1 | -1 | 0;
  onVote: (type: 1 | -1) => Promise<void>;
};

export function SpotVote({ rating, userVote, onVote }: Props) {
  return (
    <div style={{ display: 'flex', alignItems: 'center', gap: 8 }}>
      <button
		onClick={() => onVote(1)}
		disabled={userVote === 1}
		>
		👍
		</button>

		<span>{rating}</span>

		<button
		onClick={() => onVote(-1)}
		disabled={userVote === -1}
		>
		👎
		</button>
    </div>
  );
}

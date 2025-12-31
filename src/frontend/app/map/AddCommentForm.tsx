import { useState } from "react";

export type CommentDto = {
  id: number;
  text: string;
  authorEmail: string;
  createdAt: string;
};

export function AddCommentForm({ onSubmit }: { onSubmit: (text: string) => void }) {
  const [text, setText] = useState('');

  return (
    <div>
      <textarea
        value={text}
        onChange={e => setText(e.target.value)}
      />
      <button onClick={() => {
        onSubmit(text);
        setText('');
      }}>
        Отправить
      </button>
    </div>
  );
}

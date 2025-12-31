import { CommentDto } from "./AddCommentForm";

function emailToNickname(email: string) {
  return email.split('@')[0];
}

export function CommentsList({ comments }: { comments: CommentDto[] }) {
  return (
    <div style={{ maxHeight: 180, overflowY: 'auto' }}>
      {comments.map(c => (
        <div
          key={c.id}
          style={{
            marginBottom: 8,
            paddingBottom: 6,
            borderBottom: '1px solid #eee'
          }}
        >
          <b>{emailToNickname(c.authorEmail)}</b>
          <p style={{ margin: '4px 0' }}>{c.text}</p>
          <small>
            {new Date(c.createdAt).toLocaleString()}
          </small>
        </div>
      ))}
    </div>
  );
}

import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { getList, createListItem, updateListItem, deleteListItem } from "../api/api";
import type { ListDto } from "../api/api";

export default function ListPage() {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const [list, setList] = useState<ListDto | null>(null);
  const [newItemTitle, setNewItemTitle] = useState("");

  useEffect(() => {
    if (id) getList(Number(id)).then(setList);
  }, [id]);

  const handleAddItem = async () => {
    if (!newItemTitle.trim() || !list) return;
    await createListItem(newItemTitle.trim(), list.id);
    const updated = await getList(list.id);
    setList(updated);
    setNewItemTitle("");
  };

  const handleToggle = async (
    itemId: number,
    title: string,
    isCompleted: boolean
  ) => {
    await updateListItem(itemId, title, !isCompleted);
    const updated = await getList(list!.id);
    setList(updated);
  };

  const handleDeleteItem = async (e: React.MouseEvent, itemId: number) => {
    e.stopPropagation();
    await deleteListItem(itemId);
    const updated = await getList(list!.id);
    setList(updated);
  };

  if (!list) {
    return (
      <div style={styles.container}>
        <p style={{ color: "var(--text-muted)" }}>Loading...</p>
      </div>
    );
  }

  const completed = list.items.filter((i) => i.isCompleted).length;
  const total = list.items.length;

  return (
    <div style={styles.container}>
      <button
        onClick={() => navigate("/")}
        style={styles.backBtn}
        onMouseEnter={(e) =>
          (e.currentTarget.style.color = "var(--text)")
        }
        onMouseLeave={(e) =>
          (e.currentTarget.style.color = "var(--text-muted)")
        }
      >
        ← Back to lists
      </button>

      <header style={styles.header}>
        <h1 style={styles.title}>{list.name}</h1>
        {total > 0 && (
          <p style={styles.counter}>
            {completed} of {total} completed
          </p>
        )}
      </header>

      <div style={styles.addBar}>
        <input
          type="text"
          placeholder="Add a new item..."
          value={newItemTitle}
          onChange={(e) => setNewItemTitle(e.target.value)}
          onKeyDown={(e) => e.key === "Enter" && handleAddItem()}
          style={styles.input}
        />
        <button onClick={handleAddItem} style={styles.addBtn}>
          + Add
        </button>
      </div>

      <div style={styles.itemList}>
        {list.items.length === 0 ? (
          <div style={styles.empty}>
            <div style={{ fontSize: "3rem", marginBottom: "1rem" }}>✨</div>
            <p>No items yet</p>
            <p style={{ color: "var(--text-muted)", fontSize: "0.9rem" }}>
              Add your first item above
            </p>
          </div>
        ) : (
          list.items.map((item) => (
            <div
              key={item.id}
              style={{
                ...styles.item,
                opacity: item.isCompleted ? 0.6 : 1,
              }}
              onClick={() => handleToggle(item.id, item.title, item.isCompleted)}
              onMouseEnter={(e) =>
                (e.currentTarget.style.borderColor = "var(--primary)")
              }
              onMouseLeave={(e) =>
                (e.currentTarget.style.borderColor = "var(--border)")
              }
            >
              <div
                style={{
                  ...styles.checkbox,
                  backgroundColor: item.isCompleted
                    ? "var(--primary)"
                    : "transparent",
                  borderColor: item.isCompleted
                    ? "var(--primary)"
                    : "var(--text-muted)",
                }}
              >
                {item.isCompleted && (
                  <span style={{ fontSize: "0.75rem", color: "#fff" }}>✓</span>
                )}
              </div>
              <span
                style={{
                  textDecoration: item.isCompleted ? "line-through" : "none",
                  color: item.isCompleted ? "var(--text-muted)" : "var(--text)",
                  fontSize: "1rem",
                  flex: 1,
                }}
              >
                {item.title}
              </span>
              <button
                onClick={(e) => handleDeleteItem(e, item.id)}
                style={styles.deleteBtn}
                onMouseEnter={(e) => (e.currentTarget.style.color = "var(--danger)")}
                onMouseLeave={(e) => (e.currentTarget.style.color = "var(--text-muted)")}
              >
                ✕
              </button>
            </div>
          ))
        )}
      </div>
    </div>
  );
}

const styles: Record<string, React.CSSProperties> = {
  container: {
    maxWidth: 700,
    margin: "0 auto",
    padding: "2rem 1.5rem",
  },
  backBtn: {
    background: "none",
    border: "none",
    color: "var(--text-muted)",
    fontSize: "0.95rem",
    cursor: "pointer",
    padding: "0.5rem 0",
    marginBottom: "1rem",
    transition: "color 0.2s",
  },
  header: {
    marginBottom: "2rem",
  },
  title: {
    fontSize: "2.5rem",
    fontWeight: 700,
    letterSpacing: "-0.02em",
    marginBottom: "0.25rem",
  },
  counter: {
    color: "var(--text-muted)",
    fontSize: "0.95rem",
  },
  addBar: {
    display: "flex",
    gap: "0.75rem",
    marginBottom: "2rem",
  },
  input: {
    flex: 1,
    padding: "0.85rem 1rem",
    backgroundColor: "var(--surface)",
    border: "1px solid var(--border)",
    borderRadius: "var(--radius)",
    color: "var(--text)",
    fontSize: "1rem",
    outline: "none",
    transition: "border-color 0.2s",
  },
  addBtn: {
    padding: "0.85rem 1.5rem",
    backgroundColor: "var(--primary)",
    color: "#fff",
    border: "none",
    borderRadius: "var(--radius)",
    fontSize: "1rem",
    fontWeight: 600,
    cursor: "pointer",
    transition: "background-color 0.2s",
    whiteSpace: "nowrap",
  },
  itemList: {
    display: "flex",
    flexDirection: "column",
    gap: "0.5rem",
  },
  empty: {
    textAlign: "center" as const,
    padding: "4rem 2rem",
    color: "var(--text)",
    fontSize: "1.1rem",
  },
  item: {
    display: "flex",
    alignItems: "center",
    gap: "1rem",
    padding: "1rem 1.25rem",
    backgroundColor: "var(--surface)",
    border: "1px solid var(--border)",
    borderRadius: "var(--radius)",
    cursor: "pointer",
    transition: "all 0.2s ease",
    userSelect: "none" as const,
  },
  checkbox: {
    width: 22,
    height: 22,
    borderRadius: 6,
    border: "2px solid",
    display: "flex",
    alignItems: "center",
    justifyContent: "center",
    flexShrink: 0,
    transition: "all 0.2s ease",
  },
  deleteBtn: {
    background: "none",
    border: "none",
    color: "var(--text-muted)",
    fontSize: "1.1rem",
    cursor: "pointer",
    padding: "0.25rem 0.5rem",
    borderRadius: 6,
    transition: "color 0.2s",
    flexShrink: 0,
  },
};

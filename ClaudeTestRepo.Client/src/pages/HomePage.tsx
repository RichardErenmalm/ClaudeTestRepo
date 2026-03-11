import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { getLists, createList, deleteList } from "../api/api";
import type { ListDto } from "../api/api";

export default function HomePage() {
  const [lists, setLists] = useState<ListDto[]>([]);
  const [newListName, setNewListName] = useState("");
  const navigate = useNavigate();

  useEffect(() => {
    getLists().then(setLists);
  }, []);

  const handleCreate = async () => {
    if (!newListName.trim()) return;
    const created = await createList(newListName.trim());
    setLists([...lists, created]);
    setNewListName("");
  };

  const handleDelete = async (e: React.MouseEvent, id: number) => {
    e.stopPropagation();
    await deleteList(id);
    setLists(lists.filter((l) => l.id !== id));
  };

  return (
    <div style={styles.container}>
      <header style={styles.header}>
        <h1 style={styles.title}>My Lists</h1>
        <p style={styles.subtitle}>Organize your tasks effortlessly</p>
      </header>

      <div style={styles.createBar}>
        <input
          type="text"
          placeholder="Create a new list..."
          value={newListName}
          onChange={(e) => setNewListName(e.target.value)}
          onKeyDown={(e) => e.key === "Enter" && handleCreate()}
          style={styles.input}
        />
        <button onClick={handleCreate} style={styles.createBtn}>
          + Create
        </button>
      </div>

      <div style={styles.grid}>
        {lists.length === 0 ? (
          <div style={styles.empty}>
            <div style={{ fontSize: "3rem", marginBottom: "1rem" }}>📝</div>
            <p>No lists yet</p>
            <p style={{ color: "var(--text-muted)", fontSize: "0.9rem" }}>
              Create your first list to get started
            </p>
          </div>
        ) : (
          lists.map((list) => {
            const completed = list.items.filter((i) => i.isCompleted).length;
            const total = list.items.length;
            const progress = total > 0 ? (completed / total) * 100 : 0;

            return (
              <div
                key={list.id}
                onClick={() => navigate(`/list/${list.id}`)}
                style={styles.card}
                onMouseEnter={(e) => {
                  e.currentTarget.style.borderColor = "var(--primary)";
                  e.currentTarget.style.transform = "translateY(-2px)";
                }}
                onMouseLeave={(e) => {
                  e.currentTarget.style.borderColor = "var(--border)";
                  e.currentTarget.style.transform = "translateY(0)";
                }}
              >
                <div style={{ display: "flex", justifyContent: "space-between", alignItems: "center" }}>
                  <h3 style={styles.cardTitle}>{list.name}</h3>
                  <button
                    onClick={(e) => handleDelete(e, list.id)}
                    style={styles.deleteBtn}
                    onMouseEnter={(e) => (e.currentTarget.style.color = "var(--danger)")}
                    onMouseLeave={(e) => (e.currentTarget.style.color = "var(--text-muted)")}
                  >
                    ✕
                  </button>
                </div>
                <div style={styles.cardMeta}>
                  <span>
                    {total} {total === 1 ? "item" : "items"}
                  </span>
                  {total > 0 && (
                    <span>
                      {completed}/{total} done
                    </span>
                  )}
                </div>
                {total > 0 && (
                  <div style={styles.progressTrack}>
                    <div
                      style={{
                        ...styles.progressBar,
                        width: `${progress}%`,
                      }}
                    />
                  </div>
                )}
              </div>
            );
          })
        )}
      </div>
    </div>
  );
}

const styles: Record<string, React.CSSProperties> = {
  container: {
    maxWidth: 700,
    margin: "0 auto",
    padding: "3rem 1.5rem",
  },
  header: {
    marginBottom: "2.5rem",
  },
  title: {
    fontSize: "2.5rem",
    fontWeight: 700,
    letterSpacing: "-0.02em",
    marginBottom: "0.25rem",
  },
  subtitle: {
    color: "var(--text-muted)",
    fontSize: "1rem",
  },
  createBar: {
    display: "flex",
    gap: "0.75rem",
    marginBottom: "2.5rem",
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
  createBtn: {
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
  grid: {
    display: "flex",
    flexDirection: "column",
    gap: "0.75rem",
  },
  empty: {
    textAlign: "center" as const,
    padding: "4rem 2rem",
    color: "var(--text)",
    fontSize: "1.1rem",
  },
  card: {
    padding: "1.25rem 1.5rem",
    backgroundColor: "var(--surface)",
    border: "1px solid var(--border)",
    borderRadius: "var(--radius)",
    cursor: "pointer",
    transition: "all 0.2s ease",
  },
  cardTitle: {
    fontSize: "1.15rem",
    fontWeight: 600,
    marginBottom: "0.5rem",
  },
  cardMeta: {
    display: "flex",
    justifyContent: "space-between",
    color: "var(--text-muted)",
    fontSize: "0.85rem",
    marginBottom: "0.75rem",
  },
  progressTrack: {
    height: 4,
    backgroundColor: "var(--border)",
    borderRadius: 2,
    overflow: "hidden",
  },
  progressBar: {
    height: "100%",
    backgroundColor: "var(--primary)",
    borderRadius: 2,
    transition: "width 0.3s ease",
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
  },
};

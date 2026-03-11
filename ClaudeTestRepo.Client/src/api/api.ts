const BASE_URL = "https://localhost:7003/api";

export interface ListDto {
  id: number;
  name: string;
  items: ListItemDto[];
}

export interface ListItemDto {
  id: number;
  title: string;
  isCompleted: boolean;
  listId: number;
}

export async function getLists(): Promise<ListDto[]> {
  const res = await fetch(`${BASE_URL}/lists`);
  return res.json();
}

export async function getList(id: number): Promise<ListDto> {
  const res = await fetch(`${BASE_URL}/lists/${id}`);
  return res.json();
}

export async function createList(name: string): Promise<ListDto> {
  const res = await fetch(`${BASE_URL}/lists`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ name }),
  });
  return res.json();
}

export async function createListItem(title: string, listId: number): Promise<ListItemDto> {
  const res = await fetch(`${BASE_URL}/listitems`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ title, listId }),
  });
  return res.json();
}

export async function updateListItem(id: number, title: string, isCompleted: boolean): Promise<void> {
  await fetch(`${BASE_URL}/listitems/${id}`, {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ title, isCompleted }),
  });
}

export async function deleteList(id: number): Promise<void> {
  await fetch(`${BASE_URL}/lists/${id}`, { method: "DELETE" });
}

export async function deleteListItem(id: number): Promise<void> {
  await fetch(`${BASE_URL}/listitems/${id}`, { method: "DELETE" });
}

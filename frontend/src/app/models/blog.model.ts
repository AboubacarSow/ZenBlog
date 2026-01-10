import { Guid } from "./guid.type";

export interface Blog {
  id: Guid;
  title: string;
  content: string;
  imageUrl: string;
  coverImageUrl: string;
  categoryId: Guid;
  createdAt: Date;
  updatedAt: Date;
}


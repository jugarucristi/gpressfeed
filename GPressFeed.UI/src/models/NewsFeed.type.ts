import type Article from "./Article.type";

export default interface NewsFeed {
  id: string;
  publishDate: Date;
  articles: Article[];
}

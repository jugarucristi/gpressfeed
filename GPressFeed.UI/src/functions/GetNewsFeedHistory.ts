import type NewsFeed from "../models/NewsFeed.type";

export default async function GetNewsFeedHistory(
  numberOfFeeds: number
): Promise<NewsFeed[]> {
  try {
    const requestSettings = {
      method: "GET",
      headers: {
        Accept: "application/json",
      },
    };
    const response = await fetch(
      "https://www.gpressfeed.com/api/pressfeed/history?numberoffeeds=" +
        numberOfFeeds,
      requestSettings
    );

    if (!response.ok) throw new Error(`Error! status: ${response.status}`);

    const result = (await response.json()) as NewsFeed[];
    result.pop();
    result.reverse();
    return result;
  } catch {
    throw new Error("Api is down!");
  }
}

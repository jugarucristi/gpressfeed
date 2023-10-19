import { PUBLIC_IS_DEVELOPMENT_ENV } from "$env/static/public";
import { PUBLIC_GPRESSFEED_API_URL } from "$env/static/public";
import { PUBLIC_GPRESSFEED_LOCAL_API_URL } from "$env/static/public";

export default function GetGPressFeedApiUrl(): string {
  if (PUBLIC_IS_DEVELOPMENT_ENV == "True") {
    return PUBLIC_GPRESSFEED_API_URL;
  }
  return PUBLIC_GPRESSFEED_LOCAL_API_URL;
}

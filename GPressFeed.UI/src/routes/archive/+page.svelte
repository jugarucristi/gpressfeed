<script lang="ts">
  import ApiDownMessage from "../../components/ApiDownMessage.svelte";
  import ArchivedFeedList from "../../components/ArchivedFeedList.svelte";
  import Loader from "../../components/Loader.svelte";
  import GetNewsFeedHistory from "../../functions/GetNewsFeedHistory";
</script>

{#await GetNewsFeedHistory(11)}
  <Loader/>
{:then archivedFeeds}
  <ArchivedFeedList archivedFeeds={archivedFeeds}/>
{:catch}
  <ApiDownMessage/>
{/await}
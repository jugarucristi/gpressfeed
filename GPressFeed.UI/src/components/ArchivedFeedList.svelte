<script lang="ts">
  import type NewsFeed from "../models/NewsFeed.type";
  import FeedLink from "./FeedLink.svelte";

  export let archivedFeeds: NewsFeed[]; 
</script>

<style>
.feedList{
    display: flex;
    flex-direction: column;
    width: 90vw;
    margin-top: 30px;
    margin-bottom: 50px;        
}

@media screen and (max-width: 480px){
    .feedList {
    margin-top: 15px;
    margin-bottom: 50px;
    }
}
</style>

<div class="feedList">
{#each archivedFeeds as feed}
    <FeedLink feed={feed}/>
{/each}
</div>
<script lang="ts">
  import FormatDate from "../functions/FormatDate";
  import type NewsFeed from "../models/NewsFeed.type";

  export let feed: NewsFeed; 
</script>

<style>
.feedLinkWrapper{
    margin-bottom: 25px;
    border-left: 5px solid #232323;
    padding-left: 15px;
    width: 300px;
    min-height: 45px;
}

.feedLink {
    color: white;
    font-size: 30px;
    font-weight: 500;
    text-decoration: none;
    transition: 0.2s;
}   

.feedLink:hover {
    color: orange;
    transition: 0.2s;
    margin-left: 20px;
}   
</style>

<div class="feedLinkWrapper">
    <a class="feedLink" href={"/archive/" + feed.id}>
        {FormatDate(feed.publishDate)}
    </a>
</div>
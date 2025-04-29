<template>
  <div class="flex flex-col gap-4">
    <AppCard>
      <div class="prose max-w-none">
        <p>The website should provide an endpoint that returns a geojson file with building consumptions calculated.</p>
        <p>The front will display the colors of the buildings according to the consumption.</p>
      </div>
    </AppCard>
    <div id="map" class="w-full" style="height: 500px" />
  </div>
</template>

<script setup lang="ts">
import { onMounted } from 'vue';
import mapboxgl from 'mapbox-gl';
import AppCard from '@/components/AppCard.vue';

onMounted(() => {
  mapboxgl.accessToken = 'pk.eyJ1IjoiZGs2MDcxIiwiYSI6ImNtOXd0YTFzZDB1d2QyanIwNXU3dnByZGgifQ.hpO_NTVNfIzC6lVIaSNaTg';
  
  // Put mapbox code here !
});
</script>

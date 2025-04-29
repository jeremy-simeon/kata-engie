<template>
  <div class="flex flex-col gap-4">
    <AppCard>
      Lorem ipsum dolor sit amet consectetur adipisicing elit. Dolores sunt iusto nam, quo ratione
      ab reiciendis quam atque pariatur laborum nesciunt deserunt facere! Est unde voluptate magni
      rerum aliquid ratione!
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

  const map = new mapboxgl.Map({
    container: 'map',
    style: 'mapbox://styles/mapbox/streets-v12',
    center: [2.35, 48.85],
    zoom: 9,
  });

  map.on('load', () => {
    map.addSource('buildings', {
      type: 'geojson',
      data: 'https://localhost:7118/api/buildings.geojson',
    });

    map.addLayer({
      id: 'buildings-layer',
      type: 'fill',
      source: 'buildings',
      paint: {
        'fill-color': [
          'interpolate',
          ['linear'],
          ['get', 'consumption'],
          0,
          'red',
          6_000_000,
          'green',
        ],
      },
    });
  });
});
</script>

<template>
  <h1>{{ msg }}</h1>

  <div class="card">
    <button type="button" @click="count++">count is {{ count }}</button>
    <p>
      Edit
      <code>components/HelloWorld.vue</code> to test HMR
    </p>
    <button @click="getWeatherForecast">Get Weather Forecast</button>
    <div class="weather-result" v-if="weatherForecast">
      <table>
        <thead>
          <tr>
            <th>Date</th>
            <th>Summary</th>
            <th>Temperature</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="forecast in weatherForecast" :key="forecast.date">
            <td>{{ forecast.date }}</td>
            <td>{{ forecast.summary }}</td>
            <td>{{ forecast.temperature }}</td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>

  <p>
    Check out
    <a href="https://vuejs.org/guide/quick-start.html#local" target="_blank"
      >create-vue</a
    >, the official Vue + Vite starter
  </p>
  <p>
    Learn more about IDE Support for Vue in the
    <a
      href="https://vuejs.org/guide/scaling-up/tooling.html#ide-support"
      target="_blank"
      >Vue Docs Scaling up Guide</a
    >.
  </p>
  <p class="read-the-docs">Click on the Vite and Vue logos to learn more</p>
</template>

<script setup lang="ts">
import { ref } from 'vue'

defineProps<{ msg: string }>()

const count = ref(0)

interface WeatherForecast {
  date: string
  summary: string
  temperature: number
}

let weatherForecast = ref<WeatherForecast[]>([])

function getWeatherForecast() {
  console.log('Getting weather forecast...')
  // UPDATE THIS TO YOUR API URL
  fetch('https://app-otelpoc-cors.azurewebsites.net/WeatherForecast')
    .then((response) => response.json())
    .then((data) => {
      const forecast = data.properties.periods.map((period: any) => ({
        date: period.startTime,
        summary: period.shortForecast,
        temperature: period.temperature,
      }))
      weatherForecast.value = forecast
    })
}
</script>

<style scoped>
.read-the-docs {
  color: #888;
}
</style>

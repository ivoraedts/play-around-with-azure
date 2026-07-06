<template>
  <v-container class="fill-height">
    <v-responsive class="align-center text-center fill-height">
      <h1 class="text-h4 font-weight-bold mb-4">Azure API Weather Feed</h1>

      <!-- Loading State -->
      <v-progress-circular v-if="loading" indeterminate color="primary" class="mb-4"></v-progress-circular>

      <!-- Error State -->
      <v-alert v-if="error" type="error" variant="tonal" class="mb-4 text-left">
        {{ error }}
      </v-alert>

      <!-- Data Table Display -->
      <v-table v-if="weatherData.length > 0" theme="dark" class="elevation-2 rounded-lg">
        <thead>
          <tr>
            <th class="text-left font-weight-bold text-primary">Date</th>
            <th class="text-left font-weight-bold text-primary">Temp (°C)</th>
            <th class="text-left font-weight-bold text-primary">Summary</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(forecast, index) in weatherData" :key="index">
            <td>{{ forecast.date }}</td>
            <td>{{ forecast.temperatureC }}°C</td>
            <td>
              <v-chip size="small" color="secondary" variant="flat">{{ forecast.summary }}</v-chip>
            </td>
          </tr>
        </tbody>
      </v-table>

      <v-btn color="primary" class="mt-4" prepend-icon="mdi-refresh" @click="fetchWeather">
        Refresh Data
      </v-btn>
    </v-responsive>
  </v-container>
</template>

<script setup class="ts" lang="ts">
import { ref, onMounted } from 'vue'

// 1. Define a strict TypeScript interface matching your .NET Core API record structure
interface WeatherForecast {
  date: string
  temperatureC: number
  summary: string
}

// 2. Strongly type your reactive state references using generic parameters
const weatherData = ref<WeatherForecast[]>([])
const loading = ref<boolean>(false)
const error = ref<string | null>(null)

const fetchWeather = async (): Promise<void> => {
  loading.value = true
  error.value = null
  
  const baseUri = import.meta.env.VITE_API_URL as string
  
  try {
    const response = await fetch(`${baseUri}/weatherforecast`)
    if (!response.ok) throw new Error(`Server returned code: ${response.status}`)
    
    // 3. Cast your incoming JSON array explicitly to your TypeScript structure array
    const data = (await response.json()) as WeatherForecast[]
    weatherData.value = data
  } catch (err) {
    if (err instanceof Error) {
      error.value = `Failed to fetch data from API: ${err.message}`
    } else {
      error.value = 'An unknown network error occurred.'
    }
  } finally {
    loading.value = false
  }
}

onMounted((): void => {
  fetchWeather()
})
</script>

import { fileURLToPath, URL } from 'node:url'

import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [vue()],
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url))
    }
  },
  build: {
    outDir: 'docs',
    sourcemap: false,
  },
  server: {
    port: 8080
  },
  // NOTE If deploying to gh-pages uncomment the line below (https://vitejs.dev/config/shared-options.html#base)
  // base: '/allspice/'
})


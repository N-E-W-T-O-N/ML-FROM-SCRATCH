# Base path = current directory
$basePath = Get-Location

# Machine Learning categories and algorithms
$mlStructure = [ordered]@{
    "Supervised Learning" = @(
        "Linear Regression",
        "Ridge Regression",
        "Lasso Regression",
        "Elastic Net",
        "Logistic Regression",
        "Naive Bayes",
        "KNN",
        "SVM",
        "Decision Tree",
        "Random Forest",
        "Gradient Boosting",
        "AdaBoost",
        "XGBoost",
        "LightGBM",
        "CatBoost",
        "LDA",
        "QDA"
    )

    "Unsupervised Learning" = @(
        "K-Means",
        "K-Medoids",
        "Hierarchical Clustering",
        "DBSCAN",
        "HDBSCAN",
        "Mean Shift",
        "Gaussian Mixture Model",
        "Spectral Clustering",
        "PCA",
        "Kernel PCA",
        "t-SNE",
        "UMAP",
        "ICA",
        "Autoencoder"
    )

    "Reinforcement Learning" = @(
        "Q-Learning",
        "SARSA",
        "DQN",
        "Double DQN",
        "Policy Gradient",
        "A2C",
        "A3C",
        "DDPG",
        "TD3",
        "PPO",
        "SAC"
    )

    "Neural Networks" = @(
        "Perceptron",
        "MLP",
        "CNN",
        "RNN",
        "LSTM",
        "GRU"
    )

    "Transformers" = @(
        "Transformer",
        "BERT",
        "GPT",
        "Vision Transformer",
        "T5"
    )

    "Generative Models" = @(
        "GAN",
        "DCGAN",
        "CycleGAN",
        "VAE",
        "Diffusion Model"
    )

    "Probabilistic Models" = @(
        "Bayesian Network",
        "Hidden Markov Model",
        "Markov Random Field",
        "Conditional Random Field"
    )

    "Time Series" = @(
        "ARIMA",
        "SARIMA",
        "Prophet",
        "State Space Model"
    )

    "Anomaly Detection" = @(
        "Isolation Forest",
        "One-Class SVM",
        "Local Outlier Factor"
    )

    "Recommendation Systems" = @(
        "Collaborative Filtering",
        "Matrix Factorization",
        "Neural Recommender"
    )
}

# Create folders with numbering
$categoryIndex = 1

foreach ($category in $mlStructure.Keys) {
    $catPrefix = "{0:D2}" -f $categoryIndex
    $categoryPath = Join-Path $basePath "$catPrefix $category"

    if (!(Test-Path $categoryPath)) {
        New-Item -ItemType Directory -Path $categoryPath | Out-Null
    }

    $itemIndex = 1
    foreach ($item in $mlStructure[$category]) {
        $itemPrefix = "{0:D2}" -f $itemIndex
        $folderName = "$itemPrefix $item"
        $fullPath = Join-Path $categoryPath $folderName

        if (!(Test-Path $fullPath)) {
            New-Item -ItemType Directory -Path $fullPath | Out-Null
        }

        $itemIndex++
    }

    $categoryIndex++
}

Write-Host "✅ Full ML folder structure created in current directory"